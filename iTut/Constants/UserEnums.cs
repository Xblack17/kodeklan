using System.ComponentModel.DataAnnotations;

namespace iTut.Constants
{
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

    public enum Grade
    {
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve
    }
}
