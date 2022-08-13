using System;
using System.ComponentModel.DataAnnotations;


namespace iTut.Models.Edu
   
{
    public class Category
  {
        [Key]
        public int CategoryID { get; set; }
        public string EducatorID { get; set; }
        [Required]

        [Display (Name="Catetgory Name")]
        public string CategoryName { get; set; }

        public CategoryStatus categoryStatus { get; set; }
    
        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
        public enum CategoryStatus
        {
            Active,
            NotActive,
        }

    }
}
