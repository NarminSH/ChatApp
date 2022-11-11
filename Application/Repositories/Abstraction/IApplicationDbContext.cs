using System;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Post> Posts { get; set; }
    }
}

