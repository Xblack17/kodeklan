using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class School
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Display(Name = "School Location")]
        [Required]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "School Phone Primary")]
        [Required]
        public string PhonePrimary { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "School Phone Alt")]
        public string PhoneAlt { get; set; }

        [Display(Name = "School Fax")]
        public string Fax { get; set; }
        [EmailAddress]
        [Display(Name = "School Email")]
        public string Email { get; set; }

        
    }
}