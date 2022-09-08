using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GradeId { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        public string GradeName { get; set; }
    }
}
