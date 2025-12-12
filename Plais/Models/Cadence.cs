namespace PLAIS.API.Models
{
    public class Cadence
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Position { get; set; }

        public ICollection<CadenceMembership> Members { get; set; } = new List<CadenceMembership>();

    }
}
