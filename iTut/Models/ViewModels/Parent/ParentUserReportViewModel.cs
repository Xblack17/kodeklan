using iTut.Models.Parent;
using iTut.Models.Users;
using System.Collections.Generic;

namespace iTut.Models.ViewModels.Parent
{
    public class ParentUserReportViewModel
    {
        public ParentUser Parent { get; set; }
        public List<Complaint> Complaints { get; set; }
        public List<MeetingRequest> Meetings { get; set; }
    }
}
