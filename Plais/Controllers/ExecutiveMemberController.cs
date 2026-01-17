using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Cadence;
using Plais.DTOs.ExecutiveMember;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/executiveMember")]
    public class ExecutiveMemberController(IExecutiveMemberService executiveMemberService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadenceDto>>> GetAll()
        {
            var result = await executiveMemberService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExecutiveMemberDto>> GetById(int id)
        {
            var executiveMember = await executiveMemberService.GetByIdAsync(id);

            if(executiveMember == null)
            {
                return NotFound();
            }

            return Ok(executiveMember);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ExecutiveMemberDto>> Create(SaveExecutiveMemberDto dto)
        {
            var created = await executiveMemberService.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, SaveExecutiveMemberDto dto)
        {
            await executiveMemberService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await executiveMemberService.DeleteAsync(id);

            return NoContent();
        }
    }
}
