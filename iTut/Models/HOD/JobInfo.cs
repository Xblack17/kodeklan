using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class JobInfo
    {
        public int Id { get; set; }
        [Display(Name ="Select designation")]
        public int DesignationId { get; set; }
       
        public Designation Designation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Join")]
        public DateTime DOJ { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        [Display(Name = "Total Leave")]
        public int TotalLeave { get; set; }

        public byte[] Appointment { get; set; }
        public string AppointmentExt { get; set; }
        [Display(Name = "Select Employee Email")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}