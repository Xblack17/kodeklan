using System.ComponentModel.DataAnnotations;

namespace iTut.Models.Parent
{
    public class AddStudent
    {
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set; }
    }
}
