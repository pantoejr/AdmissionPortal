using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class Degree : AuditTrail
    {
        public int DepartmentID { get; set; }
        [ForeignKey(nameof(DepartmentID))]
        public Department? Department { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
