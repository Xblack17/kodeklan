using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Coordinator
{
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        public string Name { get;set;}

        [StringLength(350)]
        [Display(Name = "Report Content/description")]
        public string Description { get;set;}

        public string CreatedFor { get; set; }
        
    }
}
