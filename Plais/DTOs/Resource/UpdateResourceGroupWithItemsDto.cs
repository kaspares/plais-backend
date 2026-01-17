namespace Plais.DTOs.Resource
{
    public class UpdateResourceGroupWithItemsDto
    {
        public string Name { get; set; } = default!;
        public List<SaveResourceItemDto> Items { get; set; } = new();
    }
}
