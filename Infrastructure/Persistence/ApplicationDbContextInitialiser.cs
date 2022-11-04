//using System;
////using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace Infrastructure.Persistence;
//public class ApplicationDbContextInitialiser
//{
//    private readonly ApplicationDbContext _context;
//    //private readonly RoleManager<IdentityRole> _roleManager;
//    //private readonly UserManager<AppUser> _userManager;
//    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

//    public ApplicationDbContextInitialiser(
//        ApplicationDbContext context,
//        //RoleManager<IdentityRole> roleManager,
//        //UserManager<AppUser> userManager,
//        ILogger<ApplicationDbContextInitialiser> logger)
//    {
//        this._context = context;
//        //this._userManager = userManager;
//        //this._roleManager = roleManager;
//    }


//    public async Task InitializeAsync()
//    {
//        try
//        {
//            if (_context.Database.IsSqlServer())
//            {
//                await _context.Database.MigrateAsync();
//            }
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError("An error occured while initalising the database...");
//            throw;
//        }
//    }


//    public async Task SeedAsync()
//    {
//        try
//        {
//            await TrySeedAsync();
//        }
//        catch (Exception)
//        {
//            _logger.LogError("An error occured while seeding the database...");
//            throw;
//        }
//    }

//    public async Task TrySeedAsync()
//    {
//        // Default Roles
//        var administratorRole = new IdentityRole("administrator");
//        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
//        {
//            await _roleManager.CreateAsync(administratorRole);
//        }

//        // Default Users

//        var administrator = new AppUser { UserName = "administrator@localhost", Email = "administrator@localhost.com" };

//        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
//        {
//            await _userManager.CreateAsync(administrator, "Pa$$word1");
//            //await _userManager.AddToRoleAsync(administrator, administratorRole.Name );
//            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
//        }

//    }
//}

