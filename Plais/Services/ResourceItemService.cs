using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.Resource;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Plais.Services
{
    public class ResourceItemService(ILogger<ResourceItemService> logger,
        IMapper mapper,
        IResourceItemRepository itemRepository,
        IResourceGroupRepository rgRepository) : IResourceItemService
    {
        public async Task<ResourceItemDto> CreateItemAsync(int groupId, SaveResourceItemDto dto)
        {
            logger.LogInformation("Creating a new resource item for group ID: {GroupId}", groupId);

            var group = await rgRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new NotFoundException(nameof(ResourceGroup), groupId.ToString());
            }

            var item = mapper.Map<ResourceItem>(dto);
            item.ResourceGroupId = groupId;
            item.DateCreated = DateTime.Now;

            await itemRepository.AddAsync(item);

            return mapper.Map<ResourceItemDto>(item);
        }

        public async Task DeleteItemAsync(int itemId)
        {
            logger.LogInformation("Deleting resource item with ID: {ItemId}", itemId);

            var item = await itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new NotFoundException(nameof(ResourceItem), itemId.ToString());
            }

            await itemRepository.DeleteAsync(item);
        }

        public async Task UpdateItemAsync(int itemId, SaveResourceItemDto dto)
        {
            logger.LogInformation("Updating resource item with ID: {ItemId}", itemId);

            var item = await itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new NotFoundException(nameof(ResourceItem), itemId.ToString());
            }

            mapper.Map(dto, item);
            await itemRepository.SaveChangesAsync();
        }
    }
}
