using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.Cadence;
using Plais.Exceptions;
using Plais.Services.Interfaces;
using PLAIS.API.Models;

namespace Plais.Services
{
    public class CadenceService(ILogger<CadenceService> logger,
        ICadenceRepository cadenceRepository, IMapper mapper) : ICadenceService
    {
        public async Task<List<CadenceDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all cadences");
            var cadences = await cadenceRepository.GetAllAsync();

            var cadencesDto = mapper.Map<List<CadenceDto>>(cadences);

            cadencesDto = cadencesDto.OrderBy(c => c.Position).ToList();

            return cadencesDto;

        }

        public async Task<CadenceDto> CreateAsync(SaveCadenceDto dto)
        {
            var cadence = mapper.Map<Cadence>(dto);
            cadence.Position = 0;

            await cadenceRepository.IncrementAllPositionsAsync();

            logger.LogInformation("Creating a new cadence {@Cadence}", cadence.Name);
            await cadenceRepository.AddAsync(cadence);

             var cadenceDto = mapper.Map<CadenceDto>(cadence);

            return cadenceDto;

        }

        public async Task<CadenceDto> GetCadenceByIdAsync(int id)
        {
            logger.LogInformation("Getting cadence with id: {Id}",id);
            var cadence = await cadenceRepository.GetByIdAsync(id);

            if (cadence == null)
            {
                throw new NotFoundException(nameof(Cadence), id.ToString());
            }

            var cadenceDto = mapper.Map<CadenceDto>(cadence);

            return cadenceDto;

        }

        public async Task UpdateAsync(int id, SaveCadenceDto dto)
        {
            logger.LogInformation("Updating cadence with id: {CadenceId} with {@Cadence}", id, dto);
            var cadence = await cadenceRepository.GetByIdAsync(id);

            if (cadence == null)
            {
                throw new NotFoundException(nameof(Cadence), id.ToString());
            }


            if (cadence.Position != dto.Position)
            {
                var toSwap = await cadenceRepository.GetByPositionAsync(dto.Position);

                if (toSwap != null)
                {
                    toSwap.Position = cadence.Position;
                }

                cadence.Position = dto.Position;
            }

            cadence.Name = dto.Name;

            await cadenceRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting cadence with id: {CadenceId}", id);
            var cadence = await cadenceRepository.GetByIdAsync(id);

            if (cadence == null)
            {
                throw new NotFoundException(nameof(Cadence), id.ToString());
            }

            await cadenceRepository.DeleteAsync(cadence);

        }

        public async Task<List<CadanceWithMembersDto>> GetAllCadencesWithMembersAsync()
        {
            logger.LogInformation("Getting cadence with all members");
            var cadencesWithMembers = await cadenceRepository.GetAllCadencesWithMembersAsync();

            var cadencesWithMembersDto = mapper.Map<List<CadanceWithMembersDto>>(cadencesWithMembers);

            

            return cadencesWithMembersDto;
        }
    }
}
