public interface ICloudinaryService
{
    Task<string> UploadVideoAsync(IFormFile file);
    Task<string> UploadImageAsync(IFormFile file, string folder);
}
