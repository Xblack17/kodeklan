using iTut.Models.UserModels.EducatorUser;
using iTut.Models.UserModels.ParentUser;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.UserModels.StudentUser
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string UserId { get; set; }

        public Parent Parent { get; set; }

        public Educator Educator { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Archived { get; set; }
    }
}
