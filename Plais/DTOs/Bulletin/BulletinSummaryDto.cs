namespace Plais.DTOs.Bulletin
{
    public class BulletinSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime DateCreated { get; set; }
    }
}
