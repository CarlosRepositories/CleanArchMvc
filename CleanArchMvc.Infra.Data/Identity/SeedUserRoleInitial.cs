using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager,
              UserManager<ApplicationUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public void SeedUsers()
        {
            if (UserManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = UserManager.CreateAsync(user, "Numsey#2021").Result;

                if (result.Succeeded)
                {
                    UserManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (UserManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = UserManager.CreateAsync(user, "Numsey#2021").Result;

                if (result.Succeeded)
                {
                    UserManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

        }

        public void SeedRoles()
        {
            if (!RoleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                IdentityResult roleResult = RoleManager.CreateAsync(role).Result;
            }
            if (!RoleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = RoleManager.CreateAsync(role).Result;
            }
        }
    }
}
