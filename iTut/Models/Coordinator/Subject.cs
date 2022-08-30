using System;
using System.ComponentModel.DataAnnotations;

namespace iTut.Models.Coordinator
{
    public class Subject
    {

        [Key]
        public int SubjectId { get; set; }


        [Required]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Subject description")]
        public string SubjectDescr { get; set; }


        //  public string Educator { get; set; }

        //public string Grade { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
