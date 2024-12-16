using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace POS_System.Auth
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\POS_System\\appsettings.json");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            var connectionString = configuration.GetConnectionString("AuthDBConnectionString");

            optionsBuilder.UseNpgsql(connectionString);

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
