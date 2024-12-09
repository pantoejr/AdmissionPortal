using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class NationalityType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
