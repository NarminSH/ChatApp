using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Employee : IdentityUser
    {
        public string Fullname { get; set; } = null!;
        public string SignalRId { get; set; } = null!;
        public ICollection<Connection> Channels { get; set; }
    }
}

