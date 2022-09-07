using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public class SubjectEducator
    {
        public string SubjectEducatorId { get; set; }

        public string EducatorId { get; set; }

        public string SubjectId { get; set; }
    }
}