using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.Common;
using Plais.DTOs.Bulletin;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/bulletin")]
    public class BulletinController(IBulletinServices bulletinServices): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bulletin>>> GetAll([FromQuery] ResourceQuery query)
        {
            var result = await bulletinServices.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bulletin>> GetById(int id)
        {
            var bulletin = await bulletinServices.GetByIdAsync(id);

            return Ok(bulletin);
        }

        [HttpGet]
        [Route("latestFour")]
        public async Task<ActionResult<IEnumerable<Bulletin>>> GetFourLatest()
        {
            var result = await bulletinServices.GetFourLatestAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Bulletin>> Create(SaveBulletinDto dto)
        {
            var created = await bulletinServices.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, EditBulletinDto dto)
        {
            await bulletinServices.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await bulletinServices.DeleteAsync(id);


            return NoContent();
        }

    }
}
