using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class Shift
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}