using ERP.Application.Interfaces.Helpers;
using ERP.Core;
using Microsoft.AspNetCore.Http;

namespace ERP.Application.Helpers
{
    public class FileStorageService : IFileStorageService
    {
        public async Task SaveFileAsync(IFormFile? image, Guid? imageGuid, string directory)
        {
            string extension =Path.GetExtension(image?.FileName ?? string.Empty);

            string fileName =$"{imageGuid}{extension}";

            await using var stream =image?.OpenReadStream();

            Directory.CreateDirectory(directory);

            string filePath = Path.Combine(directory, fileName);

            await using var fileStream = new FileStream(filePath,FileMode.Create,FileAccess.Write,FileShare.None);

            await stream.CopyToAsync(fileStream);
        }


        public Task<StoredFile?> GetFileAsync(Guid fileGuid,string directory)
        {
            if (!Directory.Exists(directory))
                return Task.FromResult<StoredFile?>(null);

            string? filePath = Directory
                .GetFiles(directory, $"{fileGuid}.*")
                .FirstOrDefault();

            if (filePath == null)
                return Task.FromResult<StoredFile?>(null);

            string extension = Path.GetExtension(filePath);

            string contentType = extension.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            Stream stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            var result = new StoredFile
            {
                Stream = stream,
                FileName = Path.GetFileName(filePath),
                ContentType = contentType
            };

            return Task.FromResult<StoredFile?>(result);
        }


        public Task DeleteFileAsync(Guid? fileGuid,string directory)
        {
            if (!Directory.Exists(directory))
                return Task.CompletedTask;

            string[] files = Directory.GetFiles( directory, $"{fileGuid}.*");

            foreach (string file in files)
            {
                File.Delete(file);
            }

            return Task.CompletedTask;
        }
    }
}
