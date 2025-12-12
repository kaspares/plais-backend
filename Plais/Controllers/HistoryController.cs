using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.ByLaws;
using Plais.DTOs.History;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
        [ApiController]
        [Route("api/history")]
        public class HistoryController(IHistoryService historyService) : ControllerBase
        {
            [HttpGet]
            public async Task<ActionResult<HistoryDto>> Get()
            {
                var result = await historyService.GetAsync();
                return result;
            }

            [HttpPut]
            [Authorize]
            public async Task<IActionResult> Update(HistoryDto dto)
            {
                await historyService.UpdateAsync(dto);
                return NoContent();
            }
        }
}
