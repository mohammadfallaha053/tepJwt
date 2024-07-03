using JWT53.Models;

namespace JWT53.Services.Common;

public interface IFileService
{
    Task SaveFilesAsync(IEnumerable<IFormFile> files, string entityType, string entityId);
    Task<IEnumerable<MyFile>> GetFilesForEntityAsync(string entityType, string entityId);

    Task DeleteFileAsync(int fileId);
}
