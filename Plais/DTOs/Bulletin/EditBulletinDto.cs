namespace Plais.DTOs.Bulletin
{
    public class EditBulletinDto
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public List<string> PhotoFileNames { get; set; } = new();
        public DateTime DateCreated { get; set; }
    }
}
