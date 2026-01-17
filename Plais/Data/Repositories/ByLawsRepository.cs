using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class ByLawsRepository(PlaisDbContext dbContext) : IByLawsRepository
    {
        public async Task<ByLaws> GetAsync()
        {
            var result = await dbContext.ByLaws.FirstOrDefaultAsync();
            return result!;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
