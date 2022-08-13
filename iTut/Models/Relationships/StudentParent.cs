using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Relationships
{
    public class StudentParent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        public string ParentId { get; set; }
        public string StudentId { get; set; }
    }

    public class AddStudentModel
    {
        public string ParentId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }
    }
}
