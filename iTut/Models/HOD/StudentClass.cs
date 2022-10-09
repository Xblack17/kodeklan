using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class StudentClass
    {
        public int Id { get; set; }
        [Display(Name ="Select Class Name")]
        public int ClassNameId { get; set; }
        public ClassName ClassName { get; set; }
        [Display(Name = "Select Section")]

        public int SectionId { get; set; }
        public Section Section { get; set; }
        [Display(Name = "Select Shift")]

        public int ShiftId { get; set; }

        public Shift Shift { get; set; }


    }
}