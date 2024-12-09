using AdmissionPortal.Models;

namespace AdmissionPortal.Services
{
    public interface EMailService
    {
        bool SendMail(MailData mailData);
    }
}
