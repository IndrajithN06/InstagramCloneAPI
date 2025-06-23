using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var settings = config.GetSection("CloudinarySettings");
        var account = new Account(
            settings["CloudName"],
            settings["ApiKey"],
            settings["ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadVideoAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        await using var stream = file.OpenReadStream();

        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = $"story_videos/{Guid.NewGuid()}",
            Overwrite = true
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl?.AbsoluteUri;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folder
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.SecureUrl?.ToString();
    }
}
