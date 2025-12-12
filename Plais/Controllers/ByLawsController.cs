using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.ByLaws;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/byLaws")]
    public class ByLawsController(IByLawsService byLawsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ByLawsDto>> Get()
        {
            var result = await byLawsService.GetAsync();
            return result;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ByLawsDto dto)
        {
            await byLawsService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
