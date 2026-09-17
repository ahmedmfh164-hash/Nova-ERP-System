using Microsoft.AspNetCore.Http;
using ERP.Core;

namespace ERP.Application.Interfaces.Helpers
{
    public interface IFileStorageService
    {
        public Task SaveFileAsync(IFormFile? image, Guid? imageGuid, string directory);
       public Task<StoredFile?> GetFileAsync( Guid fileGuid,string directory); 
        public Task DeleteFileAsync(Guid? imageGuid,string directory);
    }
}
