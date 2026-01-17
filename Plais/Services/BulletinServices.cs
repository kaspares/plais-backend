using AutoMapper;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.DTOs.Bulletin;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using PLAIS.API.Models;

namespace Plais.Services
{
    public class BulletinServices(ILogger<BulletinServices> logger,
        IBulletinRepository bulletinRepository,
        IMapper mapper) : IBulletinServices
    {
        public async Task<Bulletin> CreateAsync(SaveBulletinDto dto)
        {
            logger.LogInformation("Creating a new bulletin {@Bulletin}", dto);
            var bulletin = mapper.Map<Bulletin>(dto);

            bulletin.DateCreated = DateTime.Now;

            await bulletinRepository.AddAsync(bulletin);
            return bulletin;


        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting a bulletin with id: {Bulletinid}", id);

            var bulletin = await bulletinRepository.GetByIdAsync(id);
            
            if (bulletin == null)
            {
                throw new NotFoundException(nameof(Bulletin), id.ToString());

            }

            await bulletinRepository.DeleteAsync(bulletin);
        }

        public async Task<PagedResult<Bulletin>> GetAllAsync(ResourceQuery query)
        {
            logger.LogInformation("Getting all bulletins");

            var (bulletins, totalCount) = await bulletinRepository.GetAllAsync(query);

            return new PagedResult<Bulletin>(bulletins, totalCount, query.PageSize, query.PageNumber);
        }

        public async Task<Bulletin?> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting a bulletin with id: {Id}", id);

            var bulletin = await bulletinRepository.GetByIdAsync(id);

            if (bulletin == null)
            {
                throw new NotFoundException(nameof(Bulletin), id.ToString());

            }

            return bulletin;
        }

        public async Task<List<BulletinSummaryDto>> GetFourLatestAsync()
        {
            logger.LogInformation("Getting latest bulletins");

            var bulletins = await bulletinRepository.GetFourLatestAsync();

            var bulletinsDto = mapper.Map<List<BulletinSummaryDto>>(bulletins);
            return bulletinsDto;


        }

        public async Task UpdateAsync(int id, EditBulletinDto dto)
        {
            logger.LogInformation("Updating a bulletin with id: {Id} with {@SaveBulletinDto}", id, dto);

            var bulletin = await bulletinRepository.GetByIdAsync(id);

            if (bulletin == null)
            {
                throw new NotFoundException(nameof(Bulletin), id.ToString());

            }

            mapper.Map(dto, bulletin);
            await bulletinRepository.UpdateAsync(bulletin, dto.PhotoFileNames);




        }
    }
}
