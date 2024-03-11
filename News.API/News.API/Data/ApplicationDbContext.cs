using Microsoft.EntityFrameworkCore;
using News.API.Model.Domain;

namespace News.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
