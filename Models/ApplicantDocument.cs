using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionPortal.Models
{
    public class ApplicantDocument : AuditTrail
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        public bool? IsApproved { get; set; }
        public int DocumentTypeID { get; set; }
        [ForeignKey(nameof(DocumentTypeID))]
        public DocumentType? DocumentType { get; set; }

    }
}
