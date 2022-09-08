using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public class SubjectGrade
    {
        [Key]
        public string SubjectGradeId { get; set; }

        public string SubjectId { get; set; }
        public string GradeId { get; set; }

    }
}