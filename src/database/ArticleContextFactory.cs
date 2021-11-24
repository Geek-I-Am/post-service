using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database
{
    internal class ArticlesContextFactory : IDesignTimeDbContextFactory<ArticlesContext>
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public ArticlesContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ArticlesContext>();
            builder.UseNpgsql(Configuration.GetConnectionString("postgre"));
            return new ArticlesContext(builder.Options);
        }
    }
}