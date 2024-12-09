using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class StatusType : AuditTrail
    {
        [Required]
        public string Name { get; set; } = String.Empty;
    }
}
