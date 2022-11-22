using System;
namespace Domain.Entities
{
    public class Connection: BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
    }
}

