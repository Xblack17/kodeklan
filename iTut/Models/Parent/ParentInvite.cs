using System.ComponentModel.DataAnnotations;

namespace iTut.Models.Parent
{
    public class ParentInvite
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

    }
}
