using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JwInventory.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<JwInventoryDbContext>
    {
        public JwInventoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JwInventoryDbContext>();

            // ⚠️ Connection string fixa só para o design-time  
            var connectionString = "Server=localhost;Database=JwInventoryDb;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new JwInventoryDbContext(optionsBuilder.Options);
        }
    }
}