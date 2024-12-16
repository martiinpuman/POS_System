//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace POS_System.Data
//{
//    public class AuthDbContextFactory : IDesignTimeDbContextFactory<DataDbContext>
//    {
//        //public DataDbContext CreateDbContext(string[] args)
//        //{
//        //    var configPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\POS_System\\appsettings.json");
//        //    var configuration = new ConfigurationBuilder()
//        //        .SetBasePath(Directory.GetCurrentDirectory())
//        //        .AddJsonFile(configPath)
//        //        .Build();

//        //    var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
//        //    var connectionString = configuration.GetConnectionString("AuthDBConnectionString");

//        //    optionsBuilder.UseNpgsql(connectionString);

//        //    return new DataDbContext(optionsBuilder.Options);
//        //}
//    }
//}
