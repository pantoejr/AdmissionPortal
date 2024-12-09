using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class DocumentType : AuditTrail
    {
        [Required]
        public string Name { get; set; }
    }
}
