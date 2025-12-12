using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.MainPageCarousel;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class MainPageCarouselService(ILogger<MainPageCarouselService> logger,
        IMapper mapper,
        IMainPageCarouselRepository carouselImageRepoistory) : IMainPageCarouselService
    {
        public async Task AddAsync(SaveMainPageCarouselDto dto)
        {
            logger.LogInformation("Adding new carousel image {FileName}", dto.PhotoFileName);

            var image = mapper.Map<MainPageCarousel>(dto);
            await carouselImageRepoistory.AddAsync(image);
            await carouselImageRepoistory.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting carousel image with id {Id}", id);

            var image = await carouselImageRepoistory.GetByIdAsync(id);
            if (image == null)
            {
                throw new NotFoundException(nameof(MainPageCarousel), id.ToString());
            }

            await carouselImageRepoistory.DeleteAsync(image);
            await carouselImageRepoistory.SaveChangesAsync();
        }

        public async Task<List<MainPageCarouselDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all carousel images");
            var images = await carouselImageRepoistory.GetAllAsync();

            var imagesDto = mapper.Map<List<MainPageCarouselDto>>(images);
            return imagesDto;
        }
    }
}
