using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class Department : AuditTrail
    {
        public int CollegeID { get; set; }
        [ForeignKey(nameof(CollegeID))]
        public College? College { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
