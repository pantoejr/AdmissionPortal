using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class StatusType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
    }
}
