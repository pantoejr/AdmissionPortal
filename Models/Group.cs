using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class Group : AuditTrail
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
