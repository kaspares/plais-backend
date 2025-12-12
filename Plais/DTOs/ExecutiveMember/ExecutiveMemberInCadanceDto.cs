namespace Plais.DTOs.ExecutiveMember
{
    public class ExecutiveMemberInCadanceDto
    {
        public int ExecutiveMemberId { get; set; }
        public string FullName { get; set; } = default!;
        public string Department { get; set; } = default;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string About { get; set; } = default!;
        public string Role { get; set; } = default!;
        public int Position { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
