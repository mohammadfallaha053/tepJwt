using JWT53;
using JWT53.Data;
using JWT53.Models;
using JWT53.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class FileService : IFileService
{
    private readonly ApplicationDbContext _context;
    private readonly AppSettings _appSettings;
    private readonly long _maxImageSize = 2 * 1024 * 1024; // 2 ميجابايت
    private readonly long _maxVideoSize = 5 * 1024 * 1024; // 5 ميجابايت
    private readonly List<string> _allowedImageExtensions = new List<string> { ".jpg", ".png", ".svg", ".jpeg" };
    private readonly List<string> _allowedVideoExtensions = new List<string> { ".webm", ".ogv", ".mp4", ".gltf" };
    private readonly IWebHostEnvironment _env;
    public FileService(ApplicationDbContext context, IOptions<AppSettings> appSettings, IWebHostEnvironment env)
    {
        _context = context;
        _appSettings = appSettings.Value;
        _env = env;
    }




    public async Task SaveFilesAsync(IEnumerable<IFormFile> files, string entityType, string entityId)
    {
        foreach (var file in files)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            // التحقق من امتداد الملف
            if (_allowedImageExtensions.Contains(fileExtension))
            {
                if (file.Length > _maxImageSize)
                {
                    throw new Exception("File size exceeds the allowed limit for images.");
                }
            }
            else if (_allowedVideoExtensions.Contains(fileExtension))
            {
                if (file.Length > _maxVideoSize)
                {
                    throw new Exception("File size exceeds the allowed limit for videos.");
                }
            }
            else
            {
                throw new Exception("File type is not allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine("uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileEntity = new MyFile
            {
                FilePath = filePath,
                FileType = fileExtension,
                FileSize = file.Length,
                EntityType = entityType,
                EntityId = entityId,
                FileName = file.FileName
            };

            _context.Files.Add(fileEntity);
        }
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<MyFile>> GetFilesForEntityAsync(string entityType, string entityId)
    {
        var files = await _context.Files
                                  .Where(f => f.EntityType == entityType && f.EntityId == entityId)
                                  .ToListAsync();

        files.ForEach(f => f.FilePath = $"{_appSettings.BaseUrl}/{f.FilePath}");
        return files;
    }


    public async Task DeleteFileAsync(int fileId)
    {
        var fileEntity = await _context.Files.FindAsync(fileId);
        if (fileEntity == null)
        {
            throw new Exception("File not found.");
        }

        // حذف الملف من نظام الملفات
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileEntity.FilePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // حذف الملف من قاعدة البيانات
        _context.Files.Remove(fileEntity);
        await _context.SaveChangesAsync();
    }


}
