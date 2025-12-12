namespace Plais.DTOs.Resource
{
    public class ResourceCategoryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ResourceGroupDto> Groups { get; set; } = new();
    }
}
