using iTut.Models.Parent;
using System.Threading.Tasks;

namespace iTut.Services.Implementation
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IRazorViewToStringRenderer _razorRender;

        public MailTemplateService(IRazorViewToStringRenderer razorRender)
        {
            _razorRender = razorRender;
        }

        public Task<string> GetParentInviteTemplateAsync(InviteParent model)
        {
            return _razorRender.RenderViewToStringAsync("/Templates/ParentInviteEmailTemplate.cshtml", model);
        }

        public Task<string> GetComplaintCreatedTemplateAsync(Complaint model)
        {
            return _razorRender.RenderViewToStringAsync("/Templates/ComplaintCreatedEmailTemplate.cshtml", model);
        }
    }
}
