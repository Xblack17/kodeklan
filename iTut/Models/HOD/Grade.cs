using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class Grade
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LowestMark { get; set; }
        public int HighestMark { get; set; }

    }
}