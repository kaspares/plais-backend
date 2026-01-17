using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Cadence;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/cadences")]
    public class CadenceController(ICadenceService cadenceService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadenceDto>>> GetAll()
        {
            var result = await cadenceService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CadenceDto>>> GetCadenceById(int id)
        {
            var result = await cadenceService.GetCadenceByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CadenceDto>> Create(SaveCadenceDto dto)
        {
            var result = await cadenceService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, SaveCadenceDto dto)
        {
            await cadenceService.UpdateAsync(id, dto);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await cadenceService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-members")]
        public async Task<ActionResult<IEnumerable<CadanceWithMembersDto>>> GetAllCadencesWithMembers()
        {
            var result = await cadenceService.GetAllCadencesWithMembersAsync();
            return Ok(result);
        }
    }
}
