using Coravel.Events.Interfaces;
using iTut.Models.Parent;
using iTut.Models.Users;

namespace iTut.Coravel.Events
{
    public class ComplaintCreated : IEvent
    {
        public Complaint Complaint { get; set; }
        public ParentUser Parent { get; set; }
        public ComplaintCreated(Complaint complaint, ParentUser parent)
        {
            Complaint = complaint;
            Parent = parent;
        }
    }
}
