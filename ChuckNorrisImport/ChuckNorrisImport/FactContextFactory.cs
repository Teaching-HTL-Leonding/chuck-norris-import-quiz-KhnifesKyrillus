using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChuckNorrisImport
{
    public class FactContextFactory : IDesignTimeDbContextFactory<FactContext>
    {
        public FactContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<FactContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new FactContext(optionsBuilder.Options);
        }
    }
}