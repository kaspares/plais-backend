namespace Plais.Models
{
    public class ResourceGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int ResourceCategoryId { get; set; }
        public List<ResourceItem> Items { get; set; } = new();
        public DateTime DateCreated { get; set; }

    }
}
