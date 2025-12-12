namespace Plais.DTOs.CurrentMember
{
    public class CurrentMemberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string University { get; set; } = default!;
    }
}
