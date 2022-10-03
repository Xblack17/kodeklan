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
    public enum MaritalStatus
    {
        [Display(Name = "Single")]
        Single,
        [Display(Name = "Married")]
        Married,
        [Display(Name = "Devorced")]
        Devorced,
        [Display(Name = "Widowed")]
        Widowed,
        [Display(Name = "Separeted")]
        Separeted
    }
    public enum Nationality
    {
        [Display(Name = "South African")]
        SouthAfrican,
        [Display(Name = "Other")]
        Other
       
    }
    public enum Religion
    {
        Islam = 1,
        Hinduism = 2,
        Christianity = 3,
        Buddhism = 4,
        Others = 5
    }
    public enum ResultType
        {
            Grade = 1,

            [Display(Name = "First Division")]
            FirstDivision = 2,

            [Display(Name = "Second Division")]
            SecondDivision = 3,

            [Display(Name = "Third Division")]
            ThirdDivision = 4,

            Appeared = 5,

            Enrolled = 6,

            Awarded = 7,

            Pass = 8,

            [Display(Name = "Do Not Mention")]
            DoNotMention = 9
        }
}
