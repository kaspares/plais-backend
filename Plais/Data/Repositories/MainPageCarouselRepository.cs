using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class MainPageCarouselRepository(PlaisDbContext dbContext) : IMainPageCarouselRepository
    {
        public async Task AddAsync(MainPageCarousel image)
        {
            await dbContext.MainPageCarouselImages.AddAsync(image);
        }

        public Task DeleteAsync(MainPageCarousel image)
        {
            dbContext.MainPageCarouselImages.Remove(image);
            return Task.CompletedTask;
        }

        public async Task<List<MainPageCarousel>> GetAllAsync()
        {
            var carouselImages = await dbContext.MainPageCarouselImages.ToListAsync();

            return carouselImages;
        }

        public async Task<MainPageCarousel?> GetByIdAsync(int id)
        {
            var carouselImage = await dbContext.MainPageCarouselImages.FirstOrDefaultAsync(i => i.Id == id);
            return carouselImage;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
