using FoodZOAI.FileSystem.Models.Config;
using FoodZOAI.FileSystem.Models.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Providers
{
	public class LocalFileStorageProvider : FileStorageProvider
	{
		private readonly LocalStorageConfig _config;

		public LocalFileStorageProvider(LocalStorageConfig config, ILogger<LocalFileStorageProvider> logger)
			: base(logger)
		{
			_config = config;
			EnsureDirectoryExists(_config.BasePath);
		}

		public override async Task<FileOperationResult> SaveFileAsync(string fileName, Stream fileStream, string? subPath = null)
		{
			try
			{
				var sanitizedFileName = SanitizeFileName(fileName);
				var fullPath = GetFullPath(sanitizedFileName, subPath);
				var directory = Path.GetDirectoryName(fullPath);

				if (directory != null && _config.CreateDirectoryIfNotExists)
				{
					EnsureDirectoryExists(directory);
				}

				using var fileWriteStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
				await fileStream.CopyToAsync(fileWriteStream);

				Logger.LogInformation("File saved successfully: {FilePath}", fullPath);
				return FileOperationResult.SuccessResult(fullPath, GetFileUrl(sanitizedFileName, subPath));
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error saving file: {FileName}", fileName);
				return FileOperationResult.FailureResult($"Failed to save file: {ex.Message}", ex);
			}
		}

		public override async Task<FileOperationResult> SaveFileAsync(string fileName, byte[] fileContent, string? subPath = null)
		{
			using var stream = new MemoryStream(fileContent);
			return await SaveFileAsync(fileName, stream, subPath);
		}

		public override async Task<Stream?> GetFileAsync(string fileName, string? subPath = null)
		{
			try
			{
				var fullPath = GetFullPath(fileName, subPath);
				if (!File.Exists(fullPath)) return null;

				return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error retrieving file: {FileName}", fileName);
				return null;
			}
		}

		public override async Task<bool> DeleteFileAsync(string fileName, string? subPath = null)
		{
			try
			{
				var fullPath = GetFullPath(fileName, subPath);
				if (File.Exists(fullPath))
				{
					File.Delete(fullPath);
					Logger.LogInformation("File deleted: {FilePath}", fullPath);
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error deleting file: {FileName}", fileName);
				return false;
			}
		}

		public override async Task<bool> FileExistsAsync(string fileName, string? subPath = null)
		{
			var fullPath = GetFullPath(fileName, subPath);
			return File.Exists(fullPath);
		}

		public override string GetFileUrl(string fileName, string? subPath = null)
		{
			// For local files, return the relative path or full path
			return GetFullPath(fileName, subPath);
		}

		private string GetFullPath(string fileName, string? subPath)
		{
			var filePath = BuildFilePath(fileName, subPath);
			return Path.Combine(_config.BasePath, filePath);
		}

		private void EnsureDirectoryExists(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}
