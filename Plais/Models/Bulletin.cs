namespace Plais.Models
{
    public class Bulletin
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public string Content { get; set; } = default!;

        public List<BulletinPhoto> Photos { get; set; } = new();
    }
}
