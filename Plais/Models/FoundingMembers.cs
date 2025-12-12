namespace Plais.Models
{
    public class FoundingMembers
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string University { get; set; } = default!;
    }
}
