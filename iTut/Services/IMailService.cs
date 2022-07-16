using iTut.Models.Mail;
using System.Threading.Tasks;

namespace iTut.Services
{
    public interface IMailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
