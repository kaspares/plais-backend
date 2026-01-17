using Plais.Data.Repositories;
using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IMainPageCarouselRepository
    {
        Task<List<MainPageCarousel>> GetAllAsync();
        Task<MainPageCarousel?> GetByIdAsync(int id);
        Task AddAsync(MainPageCarousel image);
        Task DeleteAsync(MainPageCarousel image);
        Task SaveChangesAsync();

    }
}
