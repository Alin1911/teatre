using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Configurations;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Repositories.Implementation;

public class FileRepository : IFileRepository
{
    public readonly string FileStoragePath;

    public static void CreateIfNotExists(string path)
    {
        if (!Directory.Exists(path)) // If the directory path doesn't exist it should be created.
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// This gets a new unique filename.
    /// When managing files the filename on your filesystem should be one decided by the application to avoid security issues and avoid overriding the files by requesting a unique filename.
    /// </summary>
    private static string NewFileName(string extension) =>
        Path.GetRandomFileName() + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + extension;

    /// <summary>
    /// Inject the required service configuration from the application.json or environment variables.
    /// </summary>
    public FileRepository(IOptions<FileStorageConfiguration> fileStorage)
    {
        FileStoragePath = fileStorage.Value.SavePath;
        CreateIfNotExists(FileStoragePath); // Create the file storage path if it doesn't exist.
    }

    public ServiceResponse<FileDTO> GetFile(string filePath, string? replacedFileName = default)
    {
        try
        {
            var path = Path.Join(FileStoragePath, filePath);

            return File.Exists(path)
                ? ServiceResponse<FileDTO>.ForSuccess(new(File.Open(path, FileMode.Open), replacedFileName ?? Path.GetFileName(filePath)))
                : ServiceResponse<FileDTO>.FromError(CommonErrors.FileNotFound);
        }
        catch
        {
            return ServiceResponse<FileDTO>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<FileDTO> SaveFileAndGet(IFormFile file, string directoryPath)
    {
        try
        {
            directoryPath = Path.Join(FileStoragePath, directoryPath);
            var extension = Path.GetExtension(file.FileName);
            var newName = NewFileName(extension);
            CreateIfNotExists(directoryPath);
            var savePath = Path.Combine(directoryPath, newName);
            var fileStream = File.Open(savePath, FileMode.CreateNew);

            file.CopyTo(fileStream);

            return ServiceResponse<FileDTO>.ForSuccess(new(fileStream, newName));
        }
        catch
        {
            return ServiceResponse<FileDTO>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<string> SaveFile(IFormFile file, string directoryPath)
    {
        try
        {
            directoryPath = Path.Join(FileStoragePath, directoryPath);
            var extension = Path.GetExtension(file.FileName);
            var newName = NewFileName(extension);
            CreateIfNotExists(directoryPath);
            var savePath = Path.Combine(directoryPath, newName);
            using var fileStream = File.Open(savePath, FileMode.CreateNew);

            file.CopyTo(fileStream);

            return ServiceResponse<string>.ForSuccess(newName);
        }
        catch
        {
            return ServiceResponse<string>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<FileDTO> SaveFileAndGet(byte[] file, string directoryPath, string fileExtension)
    {
        try
        {
            directoryPath = Path.Join(FileStoragePath, directoryPath);
            var newName = Path.GetRandomFileName() + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + fileExtension;
            CreateIfNotExists(directoryPath);
            var savePath = Path.Combine(directoryPath, newName);
            var fileStream = File.Open(savePath, FileMode.CreateNew);

            fileStream.Write(file);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<FileDTO>.ForSuccess(new(fileStream, newName));
        }
        catch
        {
            return ServiceResponse<FileDTO>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<string> SaveFile(byte[] file, string directoryPath, string fileExtension)
    {
        try
        {
            directoryPath = Path.Join(FileStoragePath, directoryPath);
            var newName = Path.GetRandomFileName() + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + fileExtension;
            CreateIfNotExists(directoryPath);
            var savePath = Path.Combine(directoryPath, newName);
            using var fileStream = File.Open(savePath, FileMode.CreateNew);

            fileStream.Write(file);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<string>.ForSuccess(newName);
        }
        catch
        {
            return ServiceResponse<string>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<FileDTO> UpdateFileAndGet(IFormFile file, string filePath)
    {
        try
        {
            filePath = Path.Join(FileStoragePath, filePath);
            var extension = Path.GetExtension(file.FileName);
            var currentExtension = Path.GetExtension(filePath);

            if (!string.Equals(extension.ToLower(), currentExtension.ToLower(), StringComparison.CurrentCultureIgnoreCase))
            {
                var newPath = filePath.Replace(currentExtension, extension);
                File.Move(filePath, newPath);
                filePath = newPath;
            }

            var fileStream = File.Open(filePath, FileMode.Truncate);

            file.CopyTo(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<FileDTO>.ForSuccess(new(fileStream, Path.GetFileName(filePath)));
        }
        catch
        {
            return ServiceResponse<FileDTO>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<string> UpdateFile(IFormFile file, string filePath)
    {
        try
        {
            filePath = Path.Join(FileStoragePath, filePath);
            var extension = Path.GetExtension(file.FileName);
            var currentExtension = Path.GetExtension(filePath);

            if (!string.Equals(extension.ToLower(), currentExtension.ToLower(), StringComparison.CurrentCultureIgnoreCase))
            {
                var newPath = filePath.Replace(currentExtension, extension);
                File.Move(filePath, newPath);
                filePath = newPath;
            }

            var fileStream = File.Open(filePath, FileMode.Truncate);

            file.CopyTo(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<string>.ForSuccess(Path.GetFileName(filePath));
        }
        catch
        {
            return ServiceResponse<string>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<FileDTO> UpdateFileAndGet(byte[] file, string filePath)
    {
        try
        {
            filePath = Path.Join(FileStoragePath, filePath);
            var fileStream = File.Open(filePath, FileMode.Truncate);

            fileStream.Write(file);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<FileDTO>.ForSuccess(new(fileStream, Path.GetFileName(filePath)));
        }
        catch
        {
            return ServiceResponse<FileDTO>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse<string> UpdateFile(byte[] file, string filePath)
    {
        try
        {
            filePath = Path.Join(FileStoragePath, filePath);
            var fileStream = File.Open(filePath, FileMode.Truncate);

            fileStream.Write(file);
            fileStream.Seek(0, SeekOrigin.Begin);

            return ServiceResponse<string>.ForSuccess(Path.GetFileName(filePath));
        }
        catch
        {
            return ServiceResponse<string>.FromError(CommonErrors.TechnicalSupport);
        }
    }

    public ServiceResponse DeleteFile(string filePath)
    {
        try
        {
            filePath = Path.Join(FileStoragePath, filePath);
            File.Delete(filePath);

            return ServiceResponse.ForSuccess();
        }
        catch
        {
            return ServiceResponse.FromError(CommonErrors.TechnicalSupport);
        }
    }
}
