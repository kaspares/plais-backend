using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using PLAIS.API.Data;
using PLAIS.API.Models;

namespace Plais.Data.Repositories
{
    public class CadenceRepository(PlaisDbContext dbContext) : ICadenceRepository
    {
        public async Task AddAsync(Cadence cadence)
        {
            await dbContext.AddAsync(cadence);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cadence cadence)
        {
            dbContext.Cadences.Remove(cadence);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Cadence>> GetAllAsync()
        {
            var cadences = await dbContext.Cadences.Include(c => c.Members).ToListAsync();

            return cadences;
        }

        public async Task<Cadence?> GetByIdAsync(int id)
        {
            var cadence = await dbContext.Cadences.Include(c => c.Members).FirstOrDefaultAsync(c => c.Id == id);

            
            return cadence; 

        }

        public async Task<List<Cadence>> GetAllCadencesWithMembersAsync()
        {
            var cadencesWithMembers = await dbContext.Cadences.Include(c => c.Members)
                .ThenInclude(cm => cm.ExecutiveMember)
                .OrderBy(c => c.Position)
                .ToListAsync();

            foreach (var cadence in cadencesWithMembers)
            {
                cadence.Members = cadence.Members
                    .OrderBy(cm => cm.Position)
                    .ToList();
            }

            return cadencesWithMembers;
        }

        public async Task<Cadence?> GetByPositionAsync(int position)
        {
            var cadenceById = await dbContext.Cadences.FirstOrDefaultAsync(c => c.Position == position);
            return cadenceById;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task IncrementAllPositionsAsync()
        {
            await dbContext.Cadences.ExecuteUpdateAsync(s => s.SetProperty(c => c.Position, c => c.Position + 1));
            await dbContext.SaveChangesAsync();
        }
    }
}
