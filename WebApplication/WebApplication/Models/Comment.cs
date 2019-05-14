using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Comment
    {
        public string GuestId { get; set; }
        public string Message { get; set; }
        public string GuestName { get; set; }
        public string CommentId { get; set; }
        public string ReplyId { get; set; }
        public List<Comment> Reply { get; set; }
    }
}