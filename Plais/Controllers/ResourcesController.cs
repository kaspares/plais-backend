using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Resource;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController(IResourceCategoryService rcService,
        IResourceGroupService rgService,
        IResourceItemService itemService) : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<ActionResult<List<ResourceCategoryDto>>> GetCategories()
        {
            var categories = await rcService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<ResourceCategoryDetailsDto>> GetCategoryDetails(int id)
        {
            var category = await rcService.GetCategoryDetailsAsync(id);
            return Ok(category); 
        }

        [HttpGet("categories/with-details")]
        public async Task<ActionResult<List<ResourceCategory>>> GetAllCategoriesWithDetails()
        {
            var categories = await rcService.GetAllCategoriesWithDetailsAsync();
            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<ActionResult<ResourceCategory>> CreateCategory(SaveResourceCategoryDto dto)
        {
            var created = await rcService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryDetails), new { id = created.Id }, created);
        }

        [HttpPut("categories/{categoryId}")]
        public async Task<IActionResult> UpdateGroup(int categoryId, SaveResourceCategoryDto dto)
        {
            await rcService.UpdateCategoryAsync(categoryId, dto);
            return NoContent();
        }

        [HttpDelete("categories")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await rcService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpPost("categories/{categoryId}/groups")]
        public async Task<ActionResult<ResourceGroupDto>> CreateGroup(int categoryId, SaveResourceGroupDto dto)
        {
            var created = await rgService.CreateGroupAsync(categoryId, dto);
            return CreatedAtAction(null, new { id = created.Id }, created);
        }

        [HttpGet("groups/{id}/details")]
        public async Task<ActionResult<ResourceGroupDto>> GetGroupWithDetails(int id)
        {
            var groupWithDetails = await rgService.GetGroupWithDetailsByIdAsync(id);
            return Ok(groupWithDetails);
        }

        [HttpPut("groups/{groupId}/with-items")]
        public async Task<IActionResult> UpdateGroupWithItems(int groupId, UpdateResourceGroupWithItemsDto dto)
        {
            await rgService.UpdateGroupWithItemsAsync(groupId, dto);
            return NoContent();
        }

        [HttpPut("groups/{groupId}")]
        public async Task<IActionResult> UpdateGroup(int groupId, SaveResourceGroupDto dto)
        {
            await rgService.UpdateGroupAsync(groupId, dto);
            return NoContent();
        }

        [HttpDelete("groups/{groupId}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            await rgService.DeleteGroupAsync(groupId);
            return NoContent();
        }

        [HttpPost("groups/{groupId}/items")]
        public async Task<ActionResult<ResourceItemDto>> CreateItem(int groupId, SaveResourceItemDto dto)
        {
            var created = await itemService.CreateItemAsync(groupId, dto);
            return CreatedAtAction(null, new { id = created.Id }, created);
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, SaveResourceItemDto dto)
        {
            await itemService.UpdateItemAsync(itemId, dto);
            return NoContent();
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            await itemService.DeleteItemAsync(itemId);
            return NoContent();
        }


    }
}
