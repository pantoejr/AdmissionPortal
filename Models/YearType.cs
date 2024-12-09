using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class YearType : AuditTrail
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
