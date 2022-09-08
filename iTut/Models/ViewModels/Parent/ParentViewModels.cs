using iTut.Models.Parent;
using System.Collections.Generic;

namespace iTut.Models.ViewModels.Parent
{
    public class ParentViewModels
    {
        
    }
    public class ComplaintsViewModel
    {
        public string ParentID;
        public List<Complaint> Complaints;
    }

    public class CreateComplaintViewModel
    {
        public Complaint Complaint;
        public string ParentID;
    }
}
