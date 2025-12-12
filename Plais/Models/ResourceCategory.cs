namespace Plais.Models
{
    public class ResourceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<ResourceGroup> Groups { get; set; } = new();
    }
}
