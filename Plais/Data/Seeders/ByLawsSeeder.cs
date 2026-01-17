
using Microsoft.EntityFrameworkCore;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Seeders
{
    public class ByLawsSeeder(PlaisDbContext dbContext) : IByLawsSeeder
    {
        public async Task SeedAsync()
        {
            if(!await dbContext.ByLaws.AnyAsync())
            {
                var defaultByLaws = new ByLaws
                {
                    Content = ""
                };

                await dbContext.ByLaws.AddAsync(defaultByLaws);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
