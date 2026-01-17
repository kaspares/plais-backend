namespace Plais.DTOs.Resource
{
    public class ResourceGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<ResourceItemDto> Items { get; set; } = new();
    }
}
