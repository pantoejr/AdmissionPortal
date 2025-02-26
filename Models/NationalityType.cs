using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class NationalityType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
