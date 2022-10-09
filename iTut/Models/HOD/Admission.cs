using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class Admission
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-mm}", ApplyFormatInEditMode = true)]
        [Display(Name =  "Admissin Date ")]
        public DateTime? AdmissionDate { get; set; }
        [Display(Name = "Previous School Address ")]

        public string PreviousSchool { get; set; }
        [Display(Name = " Previous School Address ")]

        public string PreviousSchoolAddrs { get; set; }

        

        public string Extension { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = " Select Section ")]
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = " Select  Student Class")]
        public int StudentClassId { get; set; }

        public virtual StudentClass StudentClass { get; set; }
        [Display(Name = " Select Group ")]
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        [Display(Name = " Select Student ")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

    }
}