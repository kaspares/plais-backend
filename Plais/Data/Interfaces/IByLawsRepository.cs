using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IByLawsRepository
    {
        Task<ByLaws> GetAsync();
        Task SaveChangesAsync();

    }
}
