using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class EventGroupRepository(PlaisDbContext dbContext) : IEventGroupRepository
    {
        public async Task AddAsync(EventGroup eventGroup)
        {
            await dbContext.EventGroups.AddAsync(eventGroup);
            
        }

        public async Task DeleteAsync(EventGroup eventGroup)
        {
           dbContext.EventGroups.Remove(eventGroup);
        }

        public async Task<(List<EventGroup>, int)> GetAllAsync(ResourceQuery query)
        {
            var searchPhraseLower = query.SearchPhrase?.ToLower();

            var baseQuery = dbContext.EventGroups
                .Include(eg => eg.Events)
                .AsQueryable();

            if (searchPhraseLower != null )
            {
                baseQuery = baseQuery.Where(eg =>
                eg.Title.ToLower().Contains(searchPhraseLower) ||
                eg.Events.Any(e => e.Name.ToLower().Contains(searchPhraseLower)));
            }

            var totalCount = await baseQuery.CountAsync();


            var eventGroups = await baseQuery
                .ToListAsync();

            foreach (var group in eventGroups)
            {
                group.Events = group.Events
                    .OrderByDescending(e => e.DateCreated)
                    .ToList();
            }

            if (searchPhraseLower != null)
            {
                foreach (var group in eventGroups)
                {
                    group.Events = group.Events
                        .OrderByDescending(e => e.Name.ToLower().Contains(searchPhraseLower))
                        .ThenBy(e => e.Name)
                        .ToList();
                }

                eventGroups = eventGroups
                    .OrderByDescending(eg => eg.Title.ToLower().Contains(searchPhraseLower))
                    .ThenBy(eg => eg.Title)
                    .Skip(query.PageSize * (query.PageNumber - 1))
                    .Take(query.PageSize)
                    .ToList();
            }
            else
            {
                eventGroups = eventGroups
                .OrderByDescending(eg => eg.DateCreated)
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
            }

            return (eventGroups, totalCount);
        }

        public async Task<EventGroup?> GetByIdAsync(int id)
        {
            var eventGroup = await dbContext.EventGroups.Include(e => e.Events).FirstOrDefaultAsync(e => e.Id == id);
            return eventGroup;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
