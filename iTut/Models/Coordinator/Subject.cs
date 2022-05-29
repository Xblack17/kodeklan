using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectId { get; set; }
        

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public string SubjectDescr { get; set; }


    }
}
