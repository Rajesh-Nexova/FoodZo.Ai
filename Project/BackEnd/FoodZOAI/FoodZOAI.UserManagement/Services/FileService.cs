using FoodZOAI.UserManagement.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveUserProfilePhotoAsync(int userId, IFormFile photo)
    {
        // Check WebRootPath, fallback to wwwroot under current dir if null
        var webRoot = _environment.WebRootPath;

        if (string.IsNullOrEmpty(webRoot))
        {
            webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        // Create wwwroot folder if it doesn't exist (fallback)
        if (!Directory.Exists(webRoot))
        {
            Directory.CreateDirectory(webRoot);
        }

        // Build upload folder path
        var uploadFolder = Path.Combine(webRoot, "uploads", "users", userId.ToString());

        // Create the upload folder if it does not exist
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        // Create unique file name
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

        // Full file path
        var filePath = Path.Combine(uploadFolder, fileName);

        // Save the file to disk
        using var stream = new FileStream(filePath, FileMode.Create);
        await photo.CopyToAsync(stream);

        // Return relative path for frontend usage
        var relativePath = Path.Combine("uploads", "users", userId.ToString(), fileName).Replace("\\", "/");

        return "/" + relativePath;
    }

}
