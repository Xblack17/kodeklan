using iTut.Models.Parent;
using System.Threading.Tasks;

namespace iTut.Services
{
    public interface IMailTemplateService
    {
        Task<string> GetParentInviteTemplateAsync(InviteParent model);
        Task<string> GetComplaintCreatedTemplateAsync(Complaint model);
    }
}
