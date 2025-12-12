namespace Plais.DTOs.CadenceMembership
{
    public class CadenceMembershipDto
    {
        public string FullName { get; set; } = default!;
        public string Department { get; set; } = default!;
        public int CadenceId { get; set; }
        public int Position { get; set; }
        public string Role { get; set; } = default!;
        public string? PhotoFileName { get; set; }
    }
}
