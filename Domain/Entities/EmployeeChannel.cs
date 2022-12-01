using System;

namespace Domain.Entities
{
    public class EmployeeChannel : BaseAuditableEntity
    {
        public int ChannelId { get; set; }
        public Connection Channel { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}

