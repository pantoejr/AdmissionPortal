using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class CollegeType : AuditTrail
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
