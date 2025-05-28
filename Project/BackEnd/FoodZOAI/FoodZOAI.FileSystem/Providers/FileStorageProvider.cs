using FoodZOAI.FileSystem.Core.Contract;
using FoodZOAI.FileSystem.Models.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Providers
{
	public abstract class FileStorageProvider : IFileStorageService
	{
		protected readonly ILogger Logger;

		protected FileStorageProvider(ILogger logger)
		{
			Logger = logger;
		}

		public abstract Task<FileOperationResult> SaveFileAsync(string fileName, Stream fileStream, string? subPath = null);
		public abstract Task<FileOperationResult> SaveFileAsync(string fileName, byte[] fileContent, string? subPath = null);
		public abstract Task<Stream?> GetFileAsync(string fileName, string? subPath = null);
		public abstract Task<bool> DeleteFileAsync(string fileName, string? subPath = null);
		public abstract Task<bool> FileExistsAsync(string fileName, string? subPath = null);
		public abstract string GetFileUrl(string fileName, string? subPath = null);

		protected virtual string BuildFilePath(string fileName, string? subPath)
		{
			return string.IsNullOrEmpty(subPath) ? fileName : $"{subPath.Trim('/')}/{fileName}";
		}

		protected virtual string SanitizeFileName(string fileName)
		{
			var invalidChars = Path.GetInvalidFileNameChars();
			return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
		}
	}
}
