using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public int ChannelId { get; set; }
        public Connection? Channel { get; set; }
        public string EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string Message { get; set; }
        public int? ReplyPostId { get; set; }
        public bool IsEdited { get; set; }
        public virtual Post? ReplyPost { get; set; }
        public virtual ICollection<Post>? Children { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}

