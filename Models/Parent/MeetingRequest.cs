using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTut.Models.Parent
{
    public class MeetingRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        public string ParentId { get; set; }
        [Display(Name = "Educator")]
        public string EducatorId { get; set; }
        public string Reason { get; set; }
        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Archived { get; set; } = false;
    }

    public enum MeetingStatus
    {
        Accepted,
        Rejected,
        Pending
    }
}
