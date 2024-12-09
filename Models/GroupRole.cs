using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class GroupRole : AuditTrail
    {
        public int GroupID { get; set; }
        [ForeignKey(nameof(GroupID))]
        public Group? Group { get; set; }

        public string RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public IdentityRole? Role { get; set; }
    }
}
