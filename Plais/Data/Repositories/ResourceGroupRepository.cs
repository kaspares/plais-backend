using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class ResourceGroupRepository(PlaisDbContext dbContext) : IResourceGroupRepository
    {
        public async Task AddAsync(ResourceGroup entity)
        {
            dbContext.ResourceGroups.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ResourceGroup entity)
        {
            dbContext.ResourceGroups.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ResourceGroup?> GetByIdAsync(int id)
        {
            var resourceGroup = await dbContext.ResourceGroups
                .FirstOrDefaultAsync(rg => rg.Id == id);
            return resourceGroup;
        }

        public async Task<ResourceGroup?> GetByIdWithDetailsAsync(int id)
        {
            var resourceGroupWithDetails = await dbContext.ResourceGroups
                .Include(g => g.Items)
                .FirstOrDefaultAsync(g => g.Id == id);
            return resourceGroupWithDetails;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
