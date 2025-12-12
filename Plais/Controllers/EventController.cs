using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Event;
using Plais.DTOs.EventGroup;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController(IEventService eventService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEventById(int id)
        {
            var result = await eventService.GetEventByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("latestFour")]
        public async Task<ActionResult<IEnumerable<List<EventSummaryDto>>>> GetLatestFour()
        {
            var result = await eventService.GetLatestFour();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EventDto>> Create(SaveEventDto dto)
        {
            var result = await eventService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetEventById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, SaveEventDto dto)
        {
            await eventService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await eventService.DeleteAsync(id);

            return NoContent();
        }
    }
}

