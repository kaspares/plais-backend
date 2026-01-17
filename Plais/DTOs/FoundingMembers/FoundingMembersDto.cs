namespace Plais.DTOs.FoundingMembers
{
    public class FoundingMembersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string University { get; set; } = default!;
    }
}
