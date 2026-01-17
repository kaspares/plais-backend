using AutoMapper;
using Microsoft.Identity.Client;
using Plais.Data.Interfaces;
using Plais.DTOs.Resource;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class ResourceGroupService(ILogger<ResourceGroupService> logger,
        IMapper mapper,
        IResourceGroupRepository rgRepository,
        IResourceCategoryRepository rcRepository) : IResourceGroupService
    {
        public async Task<ResourceGroupDto> CreateGroupAsync(int categoryId, SaveResourceGroupDto dto)
        {
            logger.LogInformation("Creating a new resource group for category ID: {CategoryId}", categoryId);
            
            var category = await rcRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException(nameof(ResourceCategory), categoryId.ToString());
            }

            var group = mapper.Map<ResourceGroup>(dto);
            group.ResourceCategoryId = categoryId;
            group.DateCreated = DateTime.Now;

            await rgRepository.AddAsync(group);

            return mapper.Map<ResourceGroupDto>(group);

        }

        public async Task DeleteGroupAsync(int groupId)
        {
            logger.LogInformation("Deleting resource group with ID: {GroupId}", groupId);

            var group = await rgRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new NotFoundException(nameof(ResourceGroup), groupId.ToString());
            }

            await rgRepository.DeleteAsync(group);
        }

        public async Task UpdateGroupAsync(int groupId, SaveResourceGroupDto dto)
        {
            logger.LogInformation("Updating resource group with ID: {GroupId}", groupId);

            var group = await rgRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new NotFoundException(nameof(ResourceGroup), groupId.ToString());
            }

            mapper.Map(dto, group);
            await rgRepository.SaveChangesAsync();

        }

        public async Task<ResourceGroupDto?> GetGroupWithDetailsByIdAsync(int groupId)
        {
            logger.LogInformation("Getting group id: {GroupId} with details", groupId);

            var group = await rgRepository.GetByIdWithDetailsAsync(groupId);
            if (group == null)
            {
                throw new NotFoundException(nameof(ResourceGroup), groupId.ToString());
            }

            var groupDto = mapper.Map<ResourceGroupDto>(group);
            return groupDto;
        }

        public async Task UpdateGroupWithItemsAsync(int groupId, UpdateResourceGroupWithItemsDto dto)
        {
            logger.LogInformation("Updating resource group with ID: {GroupId}", groupId);

            var group = await rgRepository.GetByIdWithDetailsAsync(groupId);
            if (group == null)
            {
                throw new NotFoundException(nameof(ResourceGroup), groupId.ToString());
            }

            group.Items.Clear();

            mapper.Map(dto, group);

            await rgRepository.SaveChangesAsync();
        }
    }
}
