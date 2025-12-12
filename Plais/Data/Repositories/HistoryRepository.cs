using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Plais.Data.Repositories
{
    public class HistoryRepository(PlaisDbContext dbContext) : ICustomHistoryRepository
    {
        public async Task<History> GetAsync()
        {
            var result = await dbContext.Histories.FirstOrDefaultAsync();
            return result!;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
