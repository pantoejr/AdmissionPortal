using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class Token : AuditTrail
    {
        [Required]
        [StringLength(10)]
        public string Value { get; set; } = String.Empty;
        public bool HasEntered { get; set; } = false;
        public DateTime DateEntered { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
