using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IResourceCategoryRepository
    {
        Task<List<ResourceCategory>> GetAllCategoriesAsync();
        Task<List<ResourceCategory>> GetAllCategoriesWithDetailsAsync();
        Task<ResourceCategory?> GetCategoryWithDetailsAsync(int categoryId);
        Task<ResourceCategory?> GetCategoryByIdAsync(int id);
        Task AddAsync(ResourceCategory category);
        Task DeleteAsync(ResourceCategory category);
        Task SaveChangesAsync();


    }
}
