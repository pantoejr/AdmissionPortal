using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.ViewModels
{
    public class TokenViewModel
    {
        [Required]
        [MaxLength(9, ErrorMessage = "Maximun Length Exceeded")]
        public string Token { get; set; } = string.Empty;
    }
}
