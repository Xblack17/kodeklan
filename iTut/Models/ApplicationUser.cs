using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iTut.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Maiden Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string MaidenName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
    }
    public enum UserType
    {
        Student,
        Educator,
        Parent,
        HOD,
        Facilitator
    }
    public enum Gender
    {
        Female,
        Male, 
        Other
    }

    public enum Race
    {
        [Display(Name = "African")]
        African,
        [Display(Name = "White")]
        White,
        [Display(Name = "Colored")]
        Colored,
        [Display(Name = "Indian")]
        Indian,
        [Display(Name = "Other")]
        Other
    }
}
