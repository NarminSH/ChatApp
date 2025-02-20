﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Employee : IdentityUser
    {
        public string Fullname { get; set; } = null!;
        public string? SignalRId { get; set; }
        public ICollection<EmployeeChannel>? EmployeeChannels { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

