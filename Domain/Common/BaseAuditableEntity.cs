using System;
namespace Domain.Common
{
    public class BaseAuditibleEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

