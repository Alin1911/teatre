using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

/// <summary>
/// This interface provides the methods to manage files on the filesystem.
/// Note that the file and directory paths are relative and will be appended to the location where the implementation is configured to handle the requests.
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// GetFile gets an file from the filesystem and may replace the file name if replacedFileName is not null.
    /// </summary>
    public ServiceResponse<FileDTO> GetFile(string filePath, string? replacedFileName = default);
    /// <summary>
    /// SaveFileAndGet saves an file from a form to the filesystem given the directory location where to store it and returns a DTO with an open file stream to it.
    /// </summary>
    public ServiceResponse<FileDTO> SaveFileAndGet(IFormFile file, string directoryPath);
    /// <summary>
    /// SaveFile saves an file from a form to the filesystem given the directory location where to store it and returns the new file name of it.
    /// </summary>
    public ServiceResponse<string> SaveFile(IFormFile file, string directoryPath);
    /// <summary>
    /// Does the same as the overload with IFormFile but may be used if the file is a byte array.
    /// </summary>
    public ServiceResponse<FileDTO> SaveFileAndGet(byte[] file, string directoryPath, string fileExtension);
    /// <summary>
    /// Does the same as the overload with IFormFile but may be used if the file is a byte array.
    /// </summary>
    public ServiceResponse<string> SaveFile(byte[] file, string directoryPath, string fileExtension);
    /// <summary>
    /// UpdateFileAndGet updates an file from a form to the filesystem given the directory location where to store it and returns a DTO with an open file stream to it.
    /// </summary>
    public ServiceResponse<FileDTO> UpdateFileAndGet(IFormFile file, string filePath);
    /// <summary>
    /// UpdateFile saves an file from a form to the filesystem given the directory location where to store it and returns a DTO with an open file stream to it.
    /// </summary>
    public ServiceResponse<string> UpdateFile(IFormFile file, string filePath);
    /// <summary>
    /// Does the same as the overload with IFormFile but may be used if the file is a byte array.
    /// </summary>
    public ServiceResponse<FileDTO> UpdateFileAndGet(byte[] file, string filePath);
    /// <summary>
    /// Does the same as the overload with IFormFile but may be used if the file is a byte array.
    /// </summary>
    public ServiceResponse<string> UpdateFile(byte[] file, string filePath);
    /// <summary>
    /// Deletes a file given the file path.
    /// </summary>
    public ServiceResponse DeleteFile(string filePath);
}
