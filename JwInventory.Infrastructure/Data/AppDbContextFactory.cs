using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JwInventory.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<JwInventoryDbContext>
    {
        public JwInventoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JwInventoryDbContext>();

            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=JwInventoryDb;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new JwInventoryDbContext(optionsBuilder.Options);
        }
    }
}