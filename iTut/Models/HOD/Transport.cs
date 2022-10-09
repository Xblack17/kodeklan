using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class Transport
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Name Of The Company")]
        public string Name { get; set; }
        [Display(Name = "Type Of")]
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        [Display(Name ="Leaving At")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-mm}", ApplyFormatInEditMode = true)]
        public DateTime Leave { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Arriving At")]
        [DisplayFormat(DataFormatString = "{0:HH-mm}", ApplyFormatInEditMode = true)]
        public DateTime Arrival { get; set; }


    }
}