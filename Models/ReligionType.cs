using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class ReligionType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
