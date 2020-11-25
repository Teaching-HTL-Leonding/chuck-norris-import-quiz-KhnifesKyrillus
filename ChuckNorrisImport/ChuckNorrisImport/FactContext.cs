using ChuckNorrisImport.Model;
using Microsoft.EntityFrameworkCore;

namespace ChuckNorrisImport
{
    public class FactContext : DbContext
    {
        public FactContext(DbContextOptions<FactContext> options)
            : base(options)
        {
        }

        public DbSet<Fact> Facts { get; set; } = null!;
    }
}