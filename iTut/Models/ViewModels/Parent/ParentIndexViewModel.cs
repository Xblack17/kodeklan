using iTut.Models.Users;
using System.Collections.Generic;

namespace iTut.Models.ViewModels.Parent
{
    public class ParentIndexViewModel
    {
        public ParentUser Parent { get; set; }
        public List<string> Children { get; set; }
    }
}
