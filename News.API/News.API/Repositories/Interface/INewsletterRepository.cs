using News.API.Model.Domain;

namespace News.API.Repositories.Interface
{
    public interface INewsletterRepository
    {
        Task<Newsletter> CreateAsync(Newsletter newsletter);
        Task<IEnumerable<Newsletter>> GetAllAsync();
    }
}
