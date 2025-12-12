namespace Plais.Models
{
    public class EventGroup
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? PhotoFileName { get; set; }
        public List<Event> Events { get; set; } = new();
        public DateTime DateCreated { get; set; }
    }
}
