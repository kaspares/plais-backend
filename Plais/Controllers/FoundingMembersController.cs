using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.FoundingMembers;
using Plais.DTOs.CurrentMember;
using Plais.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/foundingMembers")]
    public class FoundingMembersController(IFoundingMemberService foundingMemberService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundingMembersDto>>> GetAll()
        {
            var result = await foundingMemberService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoundingMembersDto>> GetById(int id)
        {
            var member = await foundingMemberService.GetByIdAsync(id);

            return Ok(member);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FoundingMembersDto>> Create(SaveFoundingMembersDto dto)
        {
            var created = await foundingMemberService.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update(int id, SaveFoundingMembersDto dto)
        {
            await foundingMemberService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            await foundingMemberService.DeleteAsync(id);

            return NoContent();
        }
    }
}
