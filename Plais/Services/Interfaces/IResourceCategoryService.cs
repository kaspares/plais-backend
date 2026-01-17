using Plais.DTOs.Resource;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IResourceCategoryService
    {
        Task<List<ResourceCategoryDto>> GetAllCategoriesAsync();
        Task<ResourceCategoryDetailsDto?> GetCategoryDetailsAsync(int categoryId);
        Task<List<ResourceCategoryDetailsDto>> GetAllCategoriesWithDetailsAsync();
        Task<ResourceCategory> CreateCategoryAsync(SaveResourceCategoryDto dto);
        Task UpdateCategoryAsync(int id, SaveResourceCategoryDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
