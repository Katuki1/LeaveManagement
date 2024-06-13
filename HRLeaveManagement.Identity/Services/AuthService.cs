using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HRLeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtSettings jwtSettings;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, 
            IOptions<JwtSettings> jwtSettings, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings.Value;
            this.signInManager = signInManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if(user == null)
                throw new NotFoundException($"User {request.Email} not found.", request.Email);
            
            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
                throw new BadRequestException($"Password for {request.Email} not valid.");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new LoginResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName
            };

            return response;

        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Employee");
                return new RegisterResponse() { UserId = user.Id };
            }
            else
            {
                throw new BadRequestException($"{result.Errors}");
            }

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims) 
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, 
                SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        
    }
}
