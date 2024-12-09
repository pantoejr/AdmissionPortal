using AdmissionPortal.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class GroupUser : AuditTrail
    {
        public int GroupID { get; set; }
        [ForeignKey(nameof(GroupID))]
        public Group? Group { get; set; }

        public string AppUserID { get; set; }
        [ForeignKey(nameof(AppUserID))]
        public AppUser? AppUser { get; set; }
    }
}
