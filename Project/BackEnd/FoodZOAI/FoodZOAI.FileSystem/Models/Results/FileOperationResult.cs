using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Models.Results
{
	public class FileOperationResult
	{
		public bool Success { get; set; }
		public string? FilePath { get; set; }
		public string? FileUrl { get; set; }
		public string? ErrorMessage { get; set; }
		public Exception? Exception { get; set; }

		public static FileOperationResult SuccessResult(string filePath, string? fileUrl = null)
			=> new() { Success = true, FilePath = filePath, FileUrl = fileUrl };

		public static FileOperationResult FailureResult(string errorMessage, Exception? exception = null)
			=> new() { Success = false, ErrorMessage = errorMessage, Exception = exception };
	}
}
