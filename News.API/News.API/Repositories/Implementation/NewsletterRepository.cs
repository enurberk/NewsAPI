using Microsoft.EntityFrameworkCore;
using News.API.Data;
using News.API.Model.Domain;
using News.API.Repositories.Interface;

namespace News.API.Repositories.Implementation
{
    public class NewsletterRepository : INewsletterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public NewsletterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Newsletter> CreateAsync(Newsletter newsletter)
        {
            await dbContext.Newsletters.AddAsync(newsletter);
            await dbContext.SaveChangesAsync();
            return newsletter;
        }

        public async Task<IEnumerable<Newsletter>> GetAllAsync()
        {
            return await dbContext.Newsletters.Include(x => x.Categories).ToListAsync();
        }
    }
}
