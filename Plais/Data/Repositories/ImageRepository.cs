using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using PLAIS.API.Data;
using System.Collections.Generic;

namespace Plais.Data.Repositories
{
    public class ImageRepository(PlaisDbContext dbContext) : IImageRepository
    {
        public async Task<List<string>> GetUsedImagePathsAsync()
        {
            var paths = new List<string>();

            paths.AddRange(await dbContext.CadenceMemberships
                .Where(cm => !string.IsNullOrWhiteSpace(cm.PhotoFileName))
                .Select(cm => cm.PhotoFileName!)
                .ToListAsync());

            paths.AddRange(await dbContext.Bulletins
                 .Where(b => b.Photos.Any(p => !string.IsNullOrWhiteSpace(p.PhotoFileName)))
                 .SelectMany(b => b.Photos.Where(p => !string.IsNullOrWhiteSpace(p.PhotoFileName)).Select(p => p.PhotoFileName!))
                 .ToListAsync());

            paths.AddRange(await dbContext.Achievements
                .Where(a => a.Images.Any(i => !string.IsNullOrWhiteSpace(i.PhotoFileName)))
                .SelectMany(a => a.Images.Where(a => !string.IsNullOrWhiteSpace(a.PhotoFileName)).Select(a => a.PhotoFileName))
                .ToListAsync());

            paths.AddRange(await dbContext.MainPageCarouselImages
                .Where(c => !string.IsNullOrWhiteSpace(c.PhotoFileName))
                .Select(c => c.PhotoFileName!)
                .ToListAsync());

            paths.AddRange(await dbContext.EventGroups
                .Where(eg => !string.IsNullOrWhiteSpace(eg.PhotoFileName))
                .Select(eg => eg.PhotoFileName!)
                .ToListAsync());
                

            return paths;
        }
    }
}
