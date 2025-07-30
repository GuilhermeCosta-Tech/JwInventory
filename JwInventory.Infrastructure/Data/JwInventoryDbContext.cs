using Microsoft.EntityFrameworkCore;
using JwInventory.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("UserType")
                .HasValue<AdminUser>("Admin")
                .HasValue<ManagerUser>("Gerente")
                .HasValue<EmployeeUser>("Colaborador");
        }
    }
}
