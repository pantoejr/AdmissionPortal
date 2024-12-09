using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }

    }
}
