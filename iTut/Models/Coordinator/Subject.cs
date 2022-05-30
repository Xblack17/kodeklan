using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public  class Subject
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string SubjectId { get; set; }


        [Required]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Subject description")]
        public string SubjectDescr { get; set; }


    }
}
