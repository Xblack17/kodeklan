using Coravel.Events.Interfaces;
using iTut.Coravel.Events;
using iTut.Models.Mail;
using iTut.Services;
using System.Threading.Tasks;
using System;

namespace iTut.Coravel.Listeners
{
    public class ComplaintCreatedListener : IListener<ComplaintCreated>
    {
        private readonly IMailService _emailService;
        private readonly IMailTemplateService _mailTemplateService;

        public ComplaintCreatedListener(IMailService emailService, IMailTemplateService mailTemplateService)
        {
            _emailService = emailService;
            _mailTemplateService = mailTemplateService;
        }

        public async Task HandleAsync(ComplaintCreated broadcasted)
        {
            MailRequest mailRequest = new MailRequest
            {
                Receiver = broadcasted.Parent.EmailAddress,
                Subject = EmailSubject.GetComplaintCreatedSuubject(parent: broadcasted.Parent),
                Body = await _mailTemplateService.GetComplaintCreatedTemplateAsync(broadcasted.Complaint)
            };

            try
            {
                await _emailService.SendEmail(mailRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
