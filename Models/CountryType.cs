using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class CountryType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
