using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class RelationshipType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
