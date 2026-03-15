using AgendaPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Servico>()
                .Property(s => s.ValorPadrao)
                .HasPrecision(10, 2);

            builder.Entity<Agendamento>()
                .Property(a => a.Status)
                .HasMaxLength(20);

            builder.Entity<Agendamento>()
                .Property(a => a.Observacoes)
                .HasMaxLength(500);
        }
    }
}
