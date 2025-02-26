using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class OccupationType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
