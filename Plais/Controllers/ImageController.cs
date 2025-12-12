
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.UploadPhoto;
using Plais.Services.Interfaces;


namespace PLAIS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ImageController(IImageService imageService) : ControllerBase
    {
        private readonly IImageService _imageService = imageService;


        [HttpPost("executive-member")] 
        public async Task<IActionResult> UploadExecutiveMemberPhoto([FromForm] UploadPhotoDto dto)
        {
            var (fileName, url) = await _imageService.SaveExecutiveMemberPhotoAsync(dto.File);
            return Ok(new { fileName, url });
        }

        [HttpPost("bulletin")]
        public async Task<IActionResult> UploadBulletinPhoto([FromForm] UploadPhotoDto dto)
        {
            var (fileName, url) = await _imageService.SaveBulletinPhotoAsync(dto.File);
            return Ok(new { fileName, url });
        }

        [HttpDelete("bulletin")] 
        public IActionResult DeleteBulletinPhoto([FromQuery] string fileName)
        {
            _imageService.DeleteBulletinPhoto(fileName);
            return NoContent(); 
        }

        [HttpDelete("executive-member")]
        public IActionResult DeleteExecutiveMemberPhoto([FromQuery] string fileName)
        {
            _imageService.DeleteExecutiveMemberPhoto(fileName);
            return NoContent();
        }

        [HttpPost("achievement")]
        public async Task<IActionResult> UploadAchievementPhoto([FromForm] UploadPhotoDto dto)
        {
            var (fileName, url) = await _imageService.SaveAchievementPhotoAsync(dto.File);
            return Ok(new { fileName, url });
        }

        [HttpDelete("achievements")]
        public IActionResult DeleteAchievementPhoto([FromQuery] string fileName)
        {
            _imageService.DeleteAchievementPhoto(fileName);
            return NoContent();
        }

        [HttpPost("carousel")]
        public async Task<IActionResult> UploadCarouselPhoto([FromForm] UploadPhotoDto dto)
        {
            var (fileName, url) = await _imageService.SaveCarouselPhotoAsync(dto.File);
            return Ok(new { fileName, url });
        }

        [HttpDelete("carousel")]
        public IActionResult DeleteCarouselPhoto([FromQuery] string fileName)
        {
            _imageService.DeleteCarouselPhoto(fileName);
            return NoContent();
        }

        [HttpPost("eventGroup")]
        public async Task<IActionResult> UploadEventGroupPhoto([FromForm] UploadPhotoDto dto)
        {
            var (fileName, url) = await _imageService.SaveEventGroupPhoto(dto.File);
            return Ok(new { fileName, url });
        }

        [HttpDelete("eventGroup")]
        public IActionResult DeleteEventGroupPhoto([FromQuery] string fileName)
        {
            _imageService.DeleteEventGroupPhoto(fileName);
            return NoContent();
        }

        [HttpDelete("unused-images")]
        public async Task<IActionResult> DeleteUnusedImages()
        {
            var deletedFiles = await imageService.DeleteUnusedImagesAsync();

            return Ok(new
            {
                DeletedCount = deletedFiles.Count,
                DeletedFiles = deletedFiles
            });
        }
    }
}