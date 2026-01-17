using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.Resource;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class ResourceCategoryService(ILogger<ResourceCategoryService> logger,
        IMapper mapper,
        IResourceCategoryRepository rcRepository) : IResourceCategoryService
    {
        public async Task<ResourceCategory> CreateCategoryAsync(SaveResourceCategoryDto dto)
        {
            logger.LogInformation("Creating a new resource category");

            var category = mapper.Map<ResourceCategory>(dto);
            await rcRepository.AddAsync(category);
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            logger.LogInformation("Deleting category with ID: {CategoryId}", id);

            var category = await rcRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException(nameof(ResourceCategory), id.ToString());
            }

            await rcRepository.DeleteAsync(category);
        }

        public async Task<List<ResourceCategoryDto>> GetAllCategoriesAsync()
        {
            logger.LogInformation("Getting all categories");

            var categories = await rcRepository.GetAllCategoriesAsync();

            var categoriesDto = mapper.Map<List<ResourceCategoryDto>>(categories);
            return categoriesDto;

        }

        public async Task<List<ResourceCategoryDetailsDto>> GetAllCategoriesWithDetailsAsync()
        {
            logger.LogInformation("Getting all categories with details");

            var categories = await rcRepository.GetAllCategoriesWithDetailsAsync();

            var categoriesDto = mapper.Map<List<ResourceCategoryDetailsDto>>(categories);
            return categoriesDto;

        }



        public async Task<ResourceCategoryDetailsDto?> GetCategoryDetailsAsync(int categoryId)
        {
            logger.LogInformation("Getting category details for ID: {CategoryId}", categoryId);

            var category = await rcRepository.GetCategoryWithDetailsAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(ResourceCategory), categoryId.ToString());
            }

            var categoryDto = mapper.Map<ResourceCategoryDetailsDto>(category);
            return categoryDto;
        }

        public async Task UpdateCategoryAsync(int id, SaveResourceCategoryDto dto)
        {
           logger.LogInformation("Updating category with ID: {CategoryId}", id);
           var category = await rcRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException(nameof(ResourceCategory), id.ToString());
            }
            mapper.Map(dto, category);
            
            await rcRepository.SaveChangesAsync();
        }
    }
}
