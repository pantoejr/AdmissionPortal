using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class CountyType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
