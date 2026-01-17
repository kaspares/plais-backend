using Plais.Data.Interfaces;
using Plais.Services.Interfaces;
using PLAIS.API.Data;

namespace Plais.Services
{
    public class ImageService(IImageRepository imageRepository,
        IWebHostEnvironment env) : IImageService
    {
            private readonly string _executivePhotoFolder = "uploads/executive-members";
            private readonly string _bulletinPhotoFolder = "uploads/bulletins";
            private readonly string _achievementPhotoFolder = "uploads/achievements";
            private readonly string _carouselPhotoFolder = "uploads/carouselImages";
            private readonly string _eventGroupPhotoFolder = "uploads/eventGroups";

        public async Task<(string fileName, string url)> SaveExecutiveMemberPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var uploadsPath = Path.Combine(env.WebRootPath, _executivePhotoFolder);
            Directory.CreateDirectory(uploadsPath); 

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/{_executivePhotoFolder.Replace("\\", "/")}/{fileName}";
            return (fileName, url);
        }

        public async Task<(string fileName, string url)> SaveBulletinPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var uploadsPath = Path.Combine(env.WebRootPath, _bulletinPhotoFolder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/{_bulletinPhotoFolder.Replace("\\", "/")}/{fileName}";
            return (fileName, url);
        }

        public void DeleteBulletinPhoto(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var uploadsFolder = Path.Combine(env.WebRootPath, _bulletinPhotoFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void DeleteExecutiveMemberPhoto(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var uploadsFolder = Path.Combine(env.WebRootPath, _executivePhotoFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<List<string>> DeleteUnusedImagesAsync()
        {
            var usedFileNames = await imageRepository.GetUsedImagePathsAsync();
            var uploadsRoot = Path.Combine(env.WebRootPath, "uploads");
            var files = Directory.GetFiles(uploadsRoot, "*.*", SearchOption.AllDirectories);

            var deletedFiles = new List<string>();

            foreach (var filePath in files)
            {
                var fileName = Path.GetFileName(filePath); 

                if (!usedFileNames.Contains(fileName, StringComparer.OrdinalIgnoreCase))
                {
                    File.Delete(filePath);
                    var relativePath = filePath.Replace(env.WebRootPath, "").Replace("\\", "/");
                    deletedFiles.Add(relativePath);
                }
            }

            return deletedFiles;
        }

        public async Task<(string fileName, string url)> SaveAchievementPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var uploadsPath = Path.Combine(env.WebRootPath, _achievementPhotoFolder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/{_achievementPhotoFolder.Replace("\\", "/")}/{fileName}";
            return (fileName, url);
        }

        public void DeleteAchievementPhoto(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var uploadsFolder = Path.Combine(env.WebRootPath, _achievementPhotoFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<(string fileName, string url)> SaveCarouselPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var uploadsPath = Path.Combine(env.WebRootPath, _carouselPhotoFolder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/{_carouselPhotoFolder.Replace("\\", "/")}/{fileName}";
            return (fileName, url);
        }

        public void DeleteCarouselPhoto(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var uploadsFolder = Path.Combine(env.WebRootPath, _carouselPhotoFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<(string fileName, string url)> SaveEventGroupPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var uploadsPath = Path.Combine(env.WebRootPath, _eventGroupPhotoFolder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/{_eventGroupPhotoFolder.Replace("\\", "/")}/{fileName}";
            return (fileName, url);
        }

        public void DeleteEventGroupPhoto(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var uploadsFolder = Path.Combine(env.WebRootPath, _eventGroupPhotoFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
