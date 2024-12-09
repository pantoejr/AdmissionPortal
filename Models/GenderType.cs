using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class GenderType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
