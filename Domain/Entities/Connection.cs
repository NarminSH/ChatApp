using System;
namespace Domain.Entities
{
    public class Connection: BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<EmployeeChannel>? EmployeeChannels { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}

