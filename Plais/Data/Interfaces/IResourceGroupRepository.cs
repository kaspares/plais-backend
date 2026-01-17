using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IResourceGroupRepository
    {
        Task AddAsync(ResourceGroup entity);
        Task<ResourceGroup?> GetByIdAsync(int id);
        Task<ResourceGroup?> GetByIdWithDetailsAsync(int id);
        Task DeleteAsync(ResourceGroup entity);
        Task SaveChangesAsync();
    }
}
