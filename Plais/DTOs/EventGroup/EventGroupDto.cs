using Plais.DTOs.Event;
using Plais.Models;

namespace Plais.DTOs.EventGroup
{
    public class EventGroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? PhotoFileName { get; set; }
        public List<EventDto> Events { get; set; } = new();
    }
}
