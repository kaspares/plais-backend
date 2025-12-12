using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.Data.Interfaces;
using Plais.DTOs.MainPageCarousel;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/mainPageCarouselImages")]
    public class MainPageCarouselController(IMainPageCarouselService carouselImageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MainPageCarouselDto>>> GetAll()
        {
            var images = await carouselImageService.GetAllAsync();
            return Ok(images);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(SaveMainPageCarouselDto dto)
        {
            await carouselImageService.AddAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await carouselImageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
