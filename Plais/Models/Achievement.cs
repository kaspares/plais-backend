namespace Plais.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public string Link { get; set; } = default!;
        public List<AchievementImage> Images { get; set; } = new();
    }
}
