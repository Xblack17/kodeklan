using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using iTut.Constants;
using System.Runtime.CompilerServices;

namespace iTut.Models.HOD
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        public Gender Gender { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
        public Religion Religion { get; set; }

        public Nationality Nationality { get; set; }

         [Display(Name = "National ID No.")]
        public string NID { get; set; }

        [Display(Name = "Present Location")]
        public string PresentAddress  { get; set; }

        [Display(Name = "Parmanent Location")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public ICollection<EmployeeEducation> EmployeeEducation { get; set; }
        public ICollection<EmploymentHistory> EmploymentHistory { get; set; }

        public ICollection<JobInfo> JobInfo { get; set; }
    }
}