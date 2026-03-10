using AgendaPro.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaPro.Data.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "Profissional" };

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@agenda.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NomeCompleto = "Administrador do Sistema",
                    TipoUsuario = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var profissionalEmail = "profissional@agenda.com";
            var profissionalUser = await userManager.FindByEmailAsync(profissionalEmail);

            if (profissionalUser == null)
            {
                profissionalUser = new ApplicationUser
                {
                    UserName = profissionalEmail,
                    Email = profissionalEmail,
                    NomeCompleto = "Profissional Padrão",
                    TipoUsuario = "Profissional",
                    Especialidade = "Serviços Gerais",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(profissionalUser, "123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(profissionalUser, "Profissional");
                }
            }
        }
    }
}