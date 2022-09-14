using System.ComponentModel.DataAnnotations;

namespace iTut.Models.Parent
{
    public class InviteParent
    {
        [Required]
        public string ParentId { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string ParentEmail { get; set; }
        [Display(Name = "Invite Message")]
        public string InviteMessage { get; set; }
    }
}
