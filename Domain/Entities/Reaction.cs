using System;
namespace Domain.Entities
{
    public class Reaction: BaseAuditableEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int Emoji { get; set; }
    }
}

