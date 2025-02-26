using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class AuditTrail
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
