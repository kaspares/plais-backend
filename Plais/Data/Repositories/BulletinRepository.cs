using Microsoft.EntityFrameworkCore;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;
using PLAIS.API.Models;

namespace Plais.Data.Repositories
{
    public class BulletinRepository(PlaisDbContext dbContext) : IBulletinRepository
    {
        public async Task AddAsync(Bulletin bulletin)
        {
            await dbContext.Bulletins.AddAsync(bulletin);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Bulletin bulletin)
        {
            dbContext.Bulletins.Remove(bulletin);
            await dbContext.SaveChangesAsync();
        }

        public async Task<(List<Bulletin>, int)> GetAllAsync(ResourceQuery query)
        {
            var searchPhraseLower = query.SearchPhrase?.ToLower();

            var baseQuery = dbContext.Bulletins.Where(b => searchPhraseLower == null || b.Title.ToLower().Contains(searchPhraseLower));

            var totalCount = await baseQuery.CountAsync();

            var bulletins = await baseQuery
                .OrderByDescending(b => b.DateCreated)
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .Include(b => b.Photos)
                .ToListAsync();

            return (bulletins, totalCount);
        }

        public async Task<Bulletin?> GetByIdAsync(int id)
        {
            var bulletin = await dbContext.Bulletins.Include(b => b.Photos).FirstOrDefaultAsync(b => b.Id == id);
            return bulletin;
        }

        public async Task<List<Bulletin>> GetFourLatestAsync()
        {
            var LatestBulletins = await dbContext.Bulletins.OrderByDescending(b => b.DateCreated)
                .Take(4)
                .ToListAsync();
            return LatestBulletins;
        }

        public async Task UpdateAsync(Bulletin bulletin, List<string> photoFileNames)
        {
            var existingPhotos = await dbContext.BulletinPhoto
            .Where(p => p.BulletinId == bulletin.Id)
            .ToListAsync();

            dbContext.BulletinPhoto.RemoveRange(existingPhotos);

            bulletin.Photos = photoFileNames.Select(fileName => new BulletinPhoto
            {
                PhotoFileName = fileName,
                BulletinId = bulletin.Id
            }).ToList();

            await dbContext.SaveChangesAsync();
        }
    }
}
