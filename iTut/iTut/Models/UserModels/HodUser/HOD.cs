using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.UserModels.HodUser
{
    public class HOD
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Archived { get; set; }
    }
}
