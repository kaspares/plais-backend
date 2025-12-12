namespace Plais.DTOs.Cadence
{
    public class CadenceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Position { get; set; }
        public List<int> MemberIds { get; set; } = new();
    }
}
