using Plais.DTOs.CadenceMembership;

namespace Plais.DTOs.ExecutiveMember
{
    public class SaveExecutiveMemberDto
    {
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string About { get; set; } = default!;

        public List<SaveCadenceMembershipDto> Memberships { get; set; } = new();
    }
}
