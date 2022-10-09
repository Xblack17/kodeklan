using iTut.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class EmployeeEducation
    {
        public int Id { get; set; }
        [Display(Name = "Select Education Level")]
        public int EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }
        [Display(Name = "Select Exam Title")]

        public int ExamTitleId { get; set; }

        public ExamTitle ExamTitle { get; set; }


        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Concentration/ Major/Group ")]
        public string Major { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Institute Name ")]
        public string InstituteName  { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Select Results Type")]

        public ResultType ResultType { get; set; }

        public float CGPA { get; set; }

        public int Scale { get; set; }

        public float Marks { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Year of passing")]
        public string PassingYear  { get; set; }

        public int Duration { get; set; }

        public string Achievement { get; set; }
        [Display(Name = "Select Employee Name")]

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}