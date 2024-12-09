using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class TitleType : AuditTrail
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
