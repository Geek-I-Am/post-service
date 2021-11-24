using System.Reflection;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options) : base(options)
        {
        }
        
        public DbSet<Articles> Articles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
            modelBuilder.HasCollation("case_insensitive_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}