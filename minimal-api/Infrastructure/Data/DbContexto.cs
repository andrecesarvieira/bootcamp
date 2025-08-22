using Microsoft.EntityFrameworkCore;
using MinimalApi.Entities;

namespace MinimalApi.Infrastructure.Data
{
    public class DbContexto(IConfiguration configuracaoApp) : DbContext
    {
        private readonly IConfiguration _configuracaoApp = configuracaoApp;
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Senha = "senha123", 
                    Perfil = "Admin"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = _configuracaoApp.GetConnectionString("MySql")?.ToString();

            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}