using FoodZOAI.FileSystem.Core.Contract;
using FoodZOAI.FileSystem.Models.Results;

namespace FoodZOAI.UserManagement.Services.Implementation
{
	public class FileService
	{
		private readonly IFileStorageService _fileStorageService;
		private readonly ILogger<FileService> _logger;

		public FileService(IFileStorageService fileStorageService, ILogger<FileService> logger)
		{
			_fileStorageService = fileStorageService;
			_logger = logger;
		}

		public async Task<FileOperationResult> UploadFileAsync(string fileName, Stream fileStream, string category = "documents")
		{
			try
			{
				// Add timestamp to filename to avoid conflicts
				var timestampedFileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmss}_{fileName}";

				var result = await _fileStorageService.SaveFileAsync(timestampedFileName, fileStream, category);

				if (result.Success)
				{
					_logger.LogInformation("File uploaded successfully: {FileName} to {FilePath}",
						fileName, result.FilePath);
				}
				else
				{
					_logger.LogError("File upload failed: {FileName}. Error: {Error}",
						fileName, result.ErrorMessage);
				}

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error during file upload: {FileName}", fileName);
				return FileOperationResult.FailureResult("An unexpected error occurred during file upload", ex);
			}
		}

		public async Task<Stream?> DownloadFileAsync(string fileName, string category = "documents")
		{
			return await _fileStorageService.GetFileAsync(fileName, category);
		}

		public async Task<bool> DeleteFileAsync(string fileName, string category = "documents")
		{
			return await _fileStorageService.DeleteFileAsync(fileName, category);
		}
	}
}
