namespace Plais.Models
{
    public class ResourceItem
    {
        public int Id { get; set; }
        public int ResourceGroupId { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime DateCreated { get; set; }
    }
}
