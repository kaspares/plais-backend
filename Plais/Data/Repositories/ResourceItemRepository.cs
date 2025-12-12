using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class ResourceItemRepository(PlaisDbContext dbContext) : IResourceItemRepository
    {
        public async Task AddAsync(ResourceItem entity)
        {
            dbContext.ResourceItems.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ResourceItem entity)
        {
            dbContext.ResourceItems.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ResourceItem?> GetByIdAsync(int id)
        {
            var resourceItem = await dbContext.ResourceItems
                .FirstOrDefaultAsync(rg => rg.Id == id);
            return resourceItem;

        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
