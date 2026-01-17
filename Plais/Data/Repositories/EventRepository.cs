using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class EventRepository(PlaisDbContext dbContext) : IEventRepository
    {
        public async Task AddAsync(Event eventEntity)
        {
            await dbContext.Events.AddAsync(eventEntity);
        }

        public async Task DeleteAsync(Event eventEntity)
        {
            dbContext.Events.Remove(eventEntity);
            
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            var eventEntity = await dbContext.Events.FindAsync(id);
            return eventEntity;
        }

        public async Task<List<Event>> GetLatestFour()
        {
            var latestEvents = await dbContext.Events.OrderByDescending(e => e.DateCreated)
                .Take(4)
                .ToListAsync();
            return latestEvents;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task IncrementAllEventPositionsInGroupAsync(int eventGroupId)
        {
            await dbContext.Events
                .Where(e => e.EventGroupId == eventGroupId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(e => e.Position, e => e.Position + 1));
        }

        public async Task<Event?> GetByPositionAsync(int position)
        {
            var eventEntity = await dbContext.Events.FirstOrDefaultAsync(e => e.Position == position);
            return eventEntity;
        }
    }
}
