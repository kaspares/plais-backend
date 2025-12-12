using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.Common;
using Plais.DTOs.Achievement;
using Plais.DTOs.Bulletin;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/achievements")]
    public class AchievementController(IAchievementService achievementService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAll()
        {
            var result = await achievementService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetById(int id)
        {
            var achievement = await achievementService.GetByIdAsync(id);

            return Ok(achievement);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Achievement>> Create(SaveAchievementDto dto)
        {
            var created = await achievementService.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, SaveAchievementDto dto)
        {
            await achievementService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await achievementService.DeleteAsync(id);


            return NoContent();
        }
    }
}
