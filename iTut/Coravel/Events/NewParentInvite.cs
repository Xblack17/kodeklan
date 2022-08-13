using Coravel.Events.Interfaces;
using iTut.Models.Parent;

namespace iTut.Coravel.Events
{
    public class NewParentInvite : IEvent
    {
        public InviteParent InviteParent { get; set; }
        public NewParentInvite(InviteParent inviteParent)
        {
            InviteParent = inviteParent;
        }
    }
}
