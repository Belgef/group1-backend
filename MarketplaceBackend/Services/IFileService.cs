namespace MarketplaceBackend.Services
{
    public interface IFileService
    {
        Task<byte[]> DownloadFileAsync(string file);
        Task<bool> UploadFileAsync(IFormFile file, string name);
        Task<bool> DeleteFileAsync(string fileName);
    }
}
