namespace Plais.DTOs.Event
{
    public class SaveEventDto
    {
        public int EventGroupId { get; set; }
        public string Name { get; set; } = default!;
        public string LinkUrl { get; set; } = default!;
    }
}
