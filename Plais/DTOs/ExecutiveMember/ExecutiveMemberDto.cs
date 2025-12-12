using Plais.DTOs.CadenceMembership;

namespace Plais.DTOs.ExecutiveMember;

public class ExecutiveMemberDto
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string About { get; set; } = default!;

    public List<CadenceMembershipDto> Memberships { get; set; } = new();
}
