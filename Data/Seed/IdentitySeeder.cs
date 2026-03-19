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
                if (!await roleManager.RoleExistsAsync(role))
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

                var createAdminResult = await userManager.CreateAsync(adminUser, "Admin@123");

                if (!createAdminResult.Succeeded)
                {
                    throw new Exception("Erro ao criar usuário admin: " +
                        string.Join(", ", createAdminResult.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
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

                var createProfissionalResult = await userManager.CreateAsync(profissionalUser, "Prof@123");

                if (!createProfissionalResult.Succeeded)
                {
                    throw new Exception("Erro ao criar usuário profissional: " +
                        string.Join(", ", createProfissionalResult.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(profissionalUser, "Profissional"))
            {
                await userManager.AddToRoleAsync(profissionalUser, "Profissional");
            }
        }
    }
}