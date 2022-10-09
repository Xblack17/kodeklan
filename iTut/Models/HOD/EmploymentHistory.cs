using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTut.Models.HOD;
using System.ComponentModel.DataAnnotations;

namespace iTut.Models.HOD
{
    public class EmploymentHistory
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Required!")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Location")]

        public string CompanyLocation { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Required!")]
        public string Designation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
        [Display (Name = "Select Employee Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        
    }
}