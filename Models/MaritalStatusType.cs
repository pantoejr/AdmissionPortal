using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class MaritalStatusType : AuditTrail
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
