using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.CurrentMember;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/currentMembers")]
    public class CurrentMemberController(IMemberServices memberService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentMemberDto>>> GetAll()
        {
            var result = await memberService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentMemberDto>> GetById(int id)
        {
            var member = await memberService.GetByIdAsync(id);

            return Ok(member);
        }

        [HttpPost]
        [Authorize]
     
        public async Task<ActionResult<CurrentMemberDto>> Create(SaveCurrentMemberDto dto)
        {
            var created = await memberService.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
 
        public async Task<IActionResult> Update(int id, SaveCurrentMemberDto dto)
        {
            await memberService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
    
        public async Task<IActionResult> Delete(int id)
        {
            await memberService.DeleteAsync(id);

            return NoContent();
        }
    }
}
