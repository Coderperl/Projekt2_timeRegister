using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Customer_Project_Administration_Application.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void SeedData()
        {
            _context.Database.Migrate();
            SeedRoles();
            SeedUsers();
        }

        private void SeedUsers()
        {
            CreateUserIfNotExist("Admin@CPAS.se", "Hejsan123#",
                new[] {"Admin"});
        }

        private void CreateUserIfNotExist(string email, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(email).Result != null) return;
            var user = new IdentityUser()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }
        private void SeedRoles()
        {
            CreateRoleIfExists("Admin");
        }

        private void CreateRoleIfExists(string roleName)
        {
            if (_context.Roles.Any(r => r.Name == roleName))
                return;
            _context.Roles.Add(new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName
            });
            _context.SaveChanges();
        }
    }
}
