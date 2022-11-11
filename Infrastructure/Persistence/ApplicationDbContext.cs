using System;
using System.Reflection;
using System.Reflection.Emit;
using Domain.Entities;
using Infrastructure.Interceptors;


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Employee>, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Reaction> Reactions { get; set; }
        DbSet<EmployeeChannel> EmployeeChannels { get; set; }
        DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this._auditableEntitySaveChangesInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        

    }

}