namespace PLAIS.API.Models
{
    public class ExecutiveMember
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string About { get; set; } = default!; // <- kod HTML

        public ICollection<CadenceMembership> Memberships { get; set; } = new List<CadenceMembership>();

    }
}
