using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTut.Models.HOD
{
    public class AssignRoll
    {
        public int Id { get; set; }
        public string Roll { get; set; }

        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

        public int StudentClassId { get; set; }

        public virtual StudentClass StudentClass { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}