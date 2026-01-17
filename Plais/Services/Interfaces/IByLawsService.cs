using Plais.DTOs.ByLaws;

namespace Plais.Services.Interfaces
{
    public interface IByLawsService
    {
        Task<ByLawsDto> GetAsync();
        Task UpdateAsync(ByLawsDto dto);
    }
}
