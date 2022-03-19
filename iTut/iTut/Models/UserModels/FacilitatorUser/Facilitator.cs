using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.UserModels.FacilitatorUser
{
    public class Facilitator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Archived { get; set; }
    }
}
