using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class ExamTitle
    {
        public int Id { get; set; }

        [Display(Name = "Title Name")]
        [Required(ErrorMessage = "Required!")]
        public string TitleName { get; set; }

        [Display(Name = "Select Education Level")]
        public int EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }
    }
}