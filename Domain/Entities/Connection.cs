using System;
namespace Domain.Entities
{
    public class Connection: BaseAuditibleEntity
    {
        public string Name { get; set; }
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}

