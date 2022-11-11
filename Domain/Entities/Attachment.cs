using System;
namespace Domain.Entities
{
    public class Attachment : BaseAuditibleEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public string FileName { get; set; }
        public int Type { get; set; }
    }
}

