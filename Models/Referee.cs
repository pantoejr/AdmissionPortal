using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class Referee : AuditTrail
    {
        public int ApplicantID { get; set; }
        [ForeignKey(nameof(ApplicantID))]
        public Applicant? Applicant { get; set; }

        [Required]
        public string? FullName { get; set; } = string.Empty;
        [Required]
        public string? Institution { get; set; } = string.Empty;
        [Required]
        public string? Occupation { get; set; } = string.Empty;
        [Required]
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string? PhoneNo { get; set; } = string.Empty;
        public bool? IsApproved { get; set; }
    }
}
