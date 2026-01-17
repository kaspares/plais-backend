using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class ResourceCategoryRepository(PlaisDbContext dbContext) : IResourceCategoryRepository
    {
        public async Task AddAsync(ResourceCategory category)
        {
            dbContext.ResourceCategories.Add(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ResourceCategory category)
        {
            dbContext.ResourceCategories.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ResourceCategory>> GetAllCategoriesAsync()
        {
            var categories = await dbContext.ResourceCategories.ToListAsync();

            return categories;
        }

        public async Task<List<ResourceCategory>> GetAllCategoriesWithDetailsAsync()
        {
            var categories = await dbContext.ResourceCategories
            .Include(rc => rc.Groups)
            .ThenInclude(rg => rg.Items)
            .ToListAsync();

            foreach (var category in categories)
            {
                category.Groups = category.Groups
                    .OrderByDescending(g => g.DateCreated)
                    .ToList();

                foreach (var group in category.Groups)
                {
                    group.Items = group.Items
                        .OrderByDescending(i => i.DateCreated) 
                        .ToList();
                }
            }

            return categories;
        }

        public async Task<ResourceCategory?> GetCategoryByIdAsync(int id)
        {
            var category = await dbContext.ResourceCategories
                .FirstOrDefaultAsync(rc => rc.Id == id);
            return category;
        }

        public async Task<ResourceCategory?> GetCategoryWithDetailsAsync(int categoryId)
        {
            var category = await dbContext.ResourceCategories
                .Include(rc => rc.Groups)
                .ThenInclude(rg => rg.Items)
                .FirstOrDefaultAsync(rc => rc.Id == categoryId);

            return category;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
