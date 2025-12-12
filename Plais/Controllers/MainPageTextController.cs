using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.ByLaws;
using Plais.DTOs.MainPageText;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/mainPageText")]
    public class MainPageTextController(IMainPageTextService mainPageTextService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MainPageTextDto>>> GetAll()
        {
            var texts = await mainPageTextService.GetAllAsync();
            return Ok(texts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MainPageTextDto>> GetById(int id)
        {
            var bulletin = await mainPageTextService.GetByIdAsync(id);

            return Ok(bulletin);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateMainPageTextDto dto)
        {
            await mainPageTextService.UpdateByIdAsync(id, dto);
            return NoContent();
        }
    }
}
