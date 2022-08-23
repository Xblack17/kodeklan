using iTut.Models.Users;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace iTut.Models.ViewModels.HOD
{
    public class HODIndexViewModel
    {
        public HODUser HodUser { get; set; }
        public class CalendarEvent
        {
            public int Id { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Text { get; set; }
            public string Color { get; set; }
        }



    }

}
