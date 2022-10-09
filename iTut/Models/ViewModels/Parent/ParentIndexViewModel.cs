using iTut.Models.Shared;
using iTut.Models.Users;
using System.Collections.Generic;

namespace iTut.Models.ViewModels.Parent
{
    public class ParentIndexViewModel
    {
        public ParentUser Parent { get; set; }
        public List<TimelinePost> Posts { get; set; }
    }
}
