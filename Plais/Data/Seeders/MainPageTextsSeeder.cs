
using Microsoft.EntityFrameworkCore;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Seeders
{
    public class MainPageTextsSeeder(PlaisDbContext dbContext) : IMainPageTextsSeeder
    {
        public async Task SeedAsync()
        {
            if (!await dbContext.MainPageTexts.AnyAsync())
            {
                var mainPageTexts = new List<MainPageText>
                {
                    new MainPageText
                    {
                        Text = "About PLAIS"
                    },
                    new MainPageText
                    {
                        Text = "Wrycza history"
                    }
                };

                await dbContext.MainPageTexts.AddRangeAsync(mainPageTexts);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
