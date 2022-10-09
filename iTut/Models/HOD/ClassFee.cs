using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class ClassFee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Admission Fee")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AdmissionFee { get; set; }
        [Display(Name ="Class Name")]
        public int ClassNameId { get; set; }
        public ClassName ClassName { get; set; }
    }
}