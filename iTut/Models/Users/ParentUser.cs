﻿using iTut.Constants;
using iTut.Models.Parent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Users
{
    public class ParentUser
    {
        public ParentUser()
        {
            this.Children = new HashSet<StudentUser>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<StudentUser> Children { get; set; }

        public Gender Gender { get; set; }

        public Race Race { get; set; }

        public string PhysicalAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Archived { get; set; }
    }
}
