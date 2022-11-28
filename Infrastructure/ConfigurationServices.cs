using System;
using System.Text;
using Infrastructure.Interceptors;
using Infrastructure.Repositories.Implementation;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                       builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            serviceCollection.AddScoped<ApplicationDbContextInitialiser>();
            serviceCollection.AddScoped<AuditableEntitySaveChangesInterceptor>();
            serviceCollection.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddTransient<IDateTime, DateTimeService>();
            serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddTransient<IPostRepository, PostRepository>();
            serviceCollection.AddTransient<IAttachmentRepository, AttachmentRepository>();
            serviceCollection.AddTransient<IReactionRepository, ReactionRepository>();
            serviceCollection.AddTransient<IConnectionRepository, ConnectionRepository>();
            serviceCollection.AddTransient<IEmployeeChannelRepository, EmployeeChannelRepository>();
            serviceCollection.AddTransient<IEmailSender, SendGridEmailSender>();

            serviceCollection.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            serviceCollection.AddIdentity<Employee, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            serviceCollection.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issue"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return serviceCollection;
        }
    }
}

