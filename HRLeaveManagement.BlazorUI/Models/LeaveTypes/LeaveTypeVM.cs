using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.BlazorUI.Models.LeaveTypes
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Default Number of Days")]
        public int DefaultDays { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
