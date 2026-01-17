namespace Plais.Services.Interfaces
{
    public interface IImageService
    {
        Task<(string fileName, string url)> SaveExecutiveMemberPhotoAsync(IFormFile file);
        void DeleteExecutiveMemberPhoto(string fileName);

        Task<(string fileName, string url)> SaveBulletinPhotoAsync(IFormFile file);
        void DeleteBulletinPhoto(string fileName);

        Task<(string fileName, string url)> SaveAchievementPhotoAsync(IFormFile file);
        void DeleteAchievementPhoto(string fileName);

        Task<(string fileName, string url)> SaveCarouselPhotoAsync(IFormFile file);
        void DeleteCarouselPhoto(string fileName);

        Task<(string fileName, string url)> SaveEventGroupPhoto(IFormFile file);
        void DeleteEventGroupPhoto(string fileName);
        Task<List<string>> DeleteUnusedImagesAsync();
    }
}