namespace Plais.DTOs.EventGroup
{
    public class EditEventGroupDto
    {
        public string Title { get; set; } = default!;
        public string? PhotoFileName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
