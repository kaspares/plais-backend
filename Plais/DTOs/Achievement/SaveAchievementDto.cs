namespace Plais.DTOs.Achievement
{
    public class SaveAchievementDto
    {
        public string Text { get; set; } = default!;
        public string Link { get; set; } = default!;
        public List<string> Images { get; set; } = new();
    }
}
