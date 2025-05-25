using FoodZOAI.FileSystem.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Core.Contract
{
	public interface IFileStorageService
	{
		Task<FileOperationResult> SaveFileAsync(string fileName, Stream fileStream, string? subPath = null);
		Task<FileOperationResult> SaveFileAsync(string fileName, byte[] fileContent, string? subPath = null);
		Task<Stream?> GetFileAsync(string fileName, string? subPath = null);
		Task<bool> DeleteFileAsync(string fileName, string? subPath = null);
		Task<bool> FileExistsAsync(string fileName, string? subPath = null);
		string GetFileUrl(string fileName, string? subPath = null);
	}
}
