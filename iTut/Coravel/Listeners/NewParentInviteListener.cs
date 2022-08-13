using Coravel.Events.Interfaces;
using iTut.Coravel.Events;
using iTut.Models.Mail;
using iTut.Models.Parent;
using iTut.Services;
using System.Threading.Tasks;

namespace iTut.Coravel.Listeners
{
    public class NewParentInviteListener : IListener<NewParentInvite>
    {
        private readonly IMailService _emailService;
        private readonly IMailTemplateService _mailTemplateService;

        public NewParentInviteListener(IMailService emailService, IMailTemplateService mailTemplateService)
        {
            _emailService = emailService;
            _mailTemplateService = mailTemplateService;
        }

        public async Task HandleAsync(NewParentInvite broadcasted)
        {
            MailRequest mailRequest = new MailRequest
            {
                Receiver = broadcasted.InviteParent.ParentEmail,
                Subject = EmailSubject.GetParentInviteSubject(),
                Body = await _mailTemplateService.GetParentInviteTemplateAsync(broadcasted.InviteParent)
            };

            try
            {
                await _emailService.SendEmail(mailRequest);
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
