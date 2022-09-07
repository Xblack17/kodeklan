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
        public DateTime? AdmissionDate { get; set; }

        public string PreviousSchool { get; set; }

        public string PreviousSchoolAddrs { get; set; }

        

        public string Extension { get; set; }

        [Required(ErrorMessage = "Required!")]
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

        [Required(ErrorMessage = "Required!")]
        public int StudentClassId { get; set; }

        public virtual StudentClass StudentClass { get; set; }
        
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

    }
}