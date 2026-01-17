using Plais.DTOs.ExecutiveMember;

namespace Plais.DTOs.Cadence
{
    public class CadanceWithMembersDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int Position { get; set; }

        public List<ExecutiveMemberInCadanceDto> Members { get; set; } = new();
    }
}
