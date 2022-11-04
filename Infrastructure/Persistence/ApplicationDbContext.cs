//using System;
//using System.Reflection;
//using System.Reflection.Emit;
////using Code.Infrastructure.Persistence.Interceptors;

//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace Infrastructure.Persistence
//{
//    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
//    {
//        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
//        public ApplicationDbContext(AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
//            DbContextOptions<ApplicationDbContext> opt) : base(opt)
//        {
//            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
//        }
//        protected override void OnModelCreating(ModelBuilder builder)
//        {

//            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//            base.OnModelCreating(builder);
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.AddInterceptors(this._auditableEntitySaveChangesInterceptor);
//            base.OnConfiguring(optionsBuilder);
//        }

//    }

//}