using Plais.DTOs.MainPageCarousel;

namespace Plais.Services.Interfaces
{
    public interface IMainPageCarouselService
    {
        Task<List<MainPageCarouselDto>> GetAllAsync();
        Task AddAsync(SaveMainPageCarouselDto dto);
        Task DeleteAsync(int id);
    }
}
