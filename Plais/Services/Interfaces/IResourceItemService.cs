using Plais.DTOs.Resource;

namespace Plais.Services.Interfaces
{
    public interface IResourceItemService
    {
        Task<ResourceItemDto> CreateItemAsync(int groupId, SaveResourceItemDto dto);
        Task UpdateItemAsync(int itemId, SaveResourceItemDto dto);
        Task DeleteItemAsync(int itemId);
    }
}
