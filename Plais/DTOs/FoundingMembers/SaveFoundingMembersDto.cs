namespace Plais.DTOs.FoundingMembers
{
    public class SaveFoundingMembersDto
    {
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string University { get; set; } = default!;
    }
}
