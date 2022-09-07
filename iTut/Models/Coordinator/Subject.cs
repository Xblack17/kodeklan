using System;
using iTut.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace iTut.Models.Coordinator
{
    public class Subject
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        [Display(Name = "Subject description")]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Subject description")]
        public string SubjectDescr { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public List<EducatorUser> SubjectEducators { get; set; } = new List<EducatorUser>();
    }
}
