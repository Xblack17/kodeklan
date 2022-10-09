using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace iTut.Models.Shared
{
    public class TimelinePost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public List<PostComment> Comments { get; set; }
        public int Likes { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Archived { get; set; } = false;
    }

    public class PostComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        public string UserId { get; set; }
        public string CommentContent { get; set; }
        public TimelinePost Post { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Archived { get; set; } = false;
    }
}
