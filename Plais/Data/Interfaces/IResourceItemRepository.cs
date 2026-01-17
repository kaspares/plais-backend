using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IResourceItemRepository
    {
        Task AddAsync(ResourceItem entity);
        Task<ResourceItem?> GetByIdAsync(int id);
        Task DeleteAsync(ResourceItem entity);
        Task SaveChangesAsync();
    }
}
