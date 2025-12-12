
using Microsoft.EntityFrameworkCore;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Seeders
{
    public class HistorySeeder(PlaisDbContext dbContext) : IHistorySeeder
    {
        public async Task SeedAsync()
        {
            if (!await dbContext.Histories.AnyAsync())
            {
                var defaultHistory = new History
                {
                    Content = ""
                };

                await dbContext.Histories.AddAsync(defaultHistory);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
