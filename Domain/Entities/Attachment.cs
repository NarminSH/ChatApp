using System;
namespace Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public string FileName { get; set; }
        public int Type { get; set; }
    }
}

