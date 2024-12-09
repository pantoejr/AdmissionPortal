using System.ComponentModel.DataAnnotations;

namespace AdmissionPortal.Models
{
    public class MailData
    {
        [Key]
        public int Id { get; set; }
        public string? EmailToId { get; set; }
        public string? EmailToName { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailBody { get; set; }
        public DateTime DateSent { get; set; }
    }
}
