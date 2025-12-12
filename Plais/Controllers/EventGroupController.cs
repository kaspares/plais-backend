using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.Common;
using Plais.DTOs.Cadence;
using Plais.DTOs.EventGroup;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/eventGroups")]
    public class EventGroupController(IEventGroupService eventGroupService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventGroupDto>>> GetAll([FromQuery] ResourceQuery query)
        {
            var result = await eventGroupService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EventGroupDto>>> GetEventGroupById(int id)
        {
            var result = await eventGroupService.GetEventGroupByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EventGroupDto>> Create(SaveEventGroupDto dto)
        {
            var result = await eventGroupService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetEventGroupById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, SaveEventGroupDto dto)
        {
            await eventGroupService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
           await eventGroupService.DeleteAsync(id);

            return NoContent();
        }
    }
}
