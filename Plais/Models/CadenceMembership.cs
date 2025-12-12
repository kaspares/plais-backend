namespace PLAIS.API.Models
{
    public class CadenceMembership
    {
        public int ExecutiveMemberId { get; set; }
        public ExecutiveMember ExecutiveMember { get; set; } = default!;

        public int CadenceId { get; set; }
        public Cadence Cadence { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string? PhotoFileName { get; set; }
        public int Position { get; set; }
    }
}
