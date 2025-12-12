using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class MainPageTextsRepository(PlaisDbContext dbContext) : IMainPageTextsRepository
    {
        public async Task<List<MainPageText>> GetAllAsync()
        {
            var texts = await dbContext.MainPageTexts.ToListAsync();

            return texts;
        }

        public async Task<MainPageText?> GetByIdAsync(int id)
        {
            var text = await dbContext.MainPageTexts.FirstOrDefaultAsync(mpt => mpt.Id == id);

            return text;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
