using Plais.DTOs.Resource;

namespace Plais.Services.Interfaces
{
    public interface IResourceGroupService
    {
        Task<ResourceGroupDto?> GetGroupWithDetailsByIdAsync(int groupId);
        Task UpdateGroupWithItemsAsync(int groupId, UpdateResourceGroupWithItemsDto dto);
        Task<ResourceGroupDto> CreateGroupAsync(int categoryId, SaveResourceGroupDto dto);
        Task UpdateGroupAsync(int groupId, SaveResourceGroupDto dto);
        Task DeleteGroupAsync(int groupId);
    }
}
