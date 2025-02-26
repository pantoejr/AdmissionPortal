using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class TitleType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
