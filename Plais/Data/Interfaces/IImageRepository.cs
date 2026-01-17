namespace Plais.Data.Interfaces
{
    public interface IImageRepository
    {
        Task<List<string>> GetUsedImagePathsAsync();
    }
}
