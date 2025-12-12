namespace Plais.DTOs.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public int EventGroupId { get; set; }
        public string Name { get; set; } = default!;
        public string LinkUrl { get; set; } = default!;
        public DateTime DateCreated { get; set; } = default!;
    }
}
