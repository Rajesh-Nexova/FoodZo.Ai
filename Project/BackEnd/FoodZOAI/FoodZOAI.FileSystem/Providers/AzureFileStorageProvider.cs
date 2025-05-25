using Azure.Storage.Blobs;
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
	public class AzureFileStorageProvider : FileStorageProvider
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly AzureStorageConfig _config;

		public AzureFileStorageProvider(AzureStorageConfig config, ILogger<AzureFileStorageProvider> logger)
			: base(logger)
		{
			_config = config;
			_blobServiceClient = new BlobServiceClient(config.ConnectionString);
		}

		public override async Task<FileOperationResult> SaveFileAsync(string fileName, Stream fileStream, string? subPath = null)
		{
			try
			{
				var containerClient = await GetContainerClientAsync();
				var blobName = BuildFilePath(fileName, subPath);
				var blobClient = containerClient.GetBlobClient(blobName);

				await blobClient.UploadAsync(fileStream, overwrite: true);

				Logger.LogInformation("File uploaded to Azure Blob: {BlobName}", blobName);
				return FileOperationResult.SuccessResult(blobName, blobClient.Uri.ToString());
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error uploading file to Azure: {FileName}", fileName);
				return FileOperationResult.FailureResult($"Failed to upload to Azure: {ex.Message}", ex);
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
				var containerClient = await GetContainerClientAsync();
				var blobName = BuildFilePath(fileName, subPath);
				var blobClient = containerClient.GetBlobClient(blobName);

				if (await blobClient.ExistsAsync())
				{
					var response = await blobClient.DownloadStreamingAsync();
					return response.Value.Content;
				}
				return null;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error downloading file from Azure: {FileName}", fileName);
				return null;
			}
		}

		public override async Task<bool> DeleteFileAsync(string fileName, string? subPath = null)
		{
			try
			{
				var containerClient = await GetContainerClientAsync();
				var blobName = BuildFilePath(fileName, subPath);
				var blobClient = containerClient.GetBlobClient(blobName);

				var response = await blobClient.DeleteIfExistsAsync();
				if (response.Value)
				{
					Logger.LogInformation("File deleted from Azure: {BlobName}", blobName);
				}
				return response.Value;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error deleting file from Azure: {FileName}", fileName);
				return false;
			}
		}

		public override async Task<bool> FileExistsAsync(string fileName, string? subPath = null)
		{
			try
			{
				var containerClient = await GetContainerClientAsync();
				var blobName = BuildFilePath(fileName, subPath);
				var blobClient = containerClient.GetBlobClient(blobName);

				var response = await blobClient.ExistsAsync();
				return response.Value;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error checking file existence in Azure: {FileName}", fileName);
				return false;
			}
		}

		public override string GetFileUrl(string fileName, string? subPath = null)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_config.ContainerName);
			var blobName = BuildFilePath(fileName, subPath);
			var blobClient = containerClient.GetBlobClient(blobName);
			return blobClient.Uri.ToString();
		}

		private async Task<BlobContainerClient> GetContainerClientAsync()
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_config.ContainerName);
			await containerClient.CreateIfNotExistsAsync();
			return containerClient;
		}
	}
}
