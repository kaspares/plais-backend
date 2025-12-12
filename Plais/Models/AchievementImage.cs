namespace Plais.Models
{
    public class AchievementImage
    {
        public int Id { get; set; }
        public string PhotoFileName { get; set; } = default!;
        public int AchievementId { get; set; }
    }
}
