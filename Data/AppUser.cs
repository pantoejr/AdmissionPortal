using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Data
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        [StringLength(20)]
        public string LastName { get; set; } = String.Empty;
        public string? LoginHint { get; set; }
        public bool IsActive { get; set; }
    }
}
