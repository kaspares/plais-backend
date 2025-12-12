using PLAIS.API.Models;

namespace Plais.Data.Interfaces
{
    public interface ICadenceRepository
    {
        Task<List<Cadence>> GetAllAsync();
        Task<Cadence?> GetByIdAsync(int id);
        Task AddAsync(Cadence cadence);
        Task DeleteAsync(Cadence cadence);
        Task<List<Cadence>> GetAllCadencesWithMembersAsync();
        Task<Cadence?> GetByPositionAsync(int position);
        Task IncrementAllPositionsAsync();
        Task SaveChangesAsync();



    }
}
