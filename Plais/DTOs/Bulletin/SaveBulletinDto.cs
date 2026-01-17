namespace Plais.DTOs.Bulletin
{
    public class SaveBulletinDto
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public List<string> PhotoFileNames { get; set; } = new();
    }
}
