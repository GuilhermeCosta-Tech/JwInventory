using Microsoft.EntityFrameworkCore;
using JwInventory.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JwInventory.Infrastructure.Data
{
    public class JwInventoryDbContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InternalTransfer> InternalTransfers { get; set; }

        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=JwInventoryDb;Trusted_Connection=True;";

        public JwInventoryDbContext()
        {

        }

        public JwInventoryDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a hierarquia de usuários (TPH)
            modelBuilder.Entity<PessoaComAcesso>()
                .HasDiscriminator<string>("UserType")
                .HasValue<AdminUser>("Admin")
                .HasValue<ManagerUser>("Gerente")
                .HasValue<EmployeeUser>("Colaborador");

            // Configura a precisão da propriedade Preco para evitar warnings
            modelBuilder.Entity<Product>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
