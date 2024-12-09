using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class ReligionType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
