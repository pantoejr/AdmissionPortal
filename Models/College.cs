using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class College : AuditTrail
    {
        public int CollegeTypeID { get; set; }
        [ForeignKey(nameof(CollegeTypeID))]
        public CollegeType? CollegeType { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
