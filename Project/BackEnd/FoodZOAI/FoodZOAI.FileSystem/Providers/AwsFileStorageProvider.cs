using Amazon.S3;
using Amazon.S3.Model;
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
	public class AwsFileStorageProvider : FileStorageProvider
	{
		private readonly IAmazonS3 _s3Client;
		private readonly AwsStorageConfig _config;

		public AwsFileStorageProvider(AwsStorageConfig config, ILogger<AwsFileStorageProvider> logger)
			: base(logger)
		{
			_config = config;
			_s3Client = new AmazonS3Client(config.AccessKey, config.SecretKey, Amazon.RegionEndpoint.GetBySystemName(config.Region));
		}

		public override async Task<FileOperationResult> SaveFileAsync(string fileName, Stream fileStream, string? subPath = null)
		{
			try
			{
				var key = BuildFilePath(fileName, subPath);
				var request = new Amazon.S3.Model.PutObjectRequest
				{
					BucketName = _config.BucketName,
					Key = key,
					InputStream = fileStream,
					ContentType = GetContentType(fileName)
				};

				await _s3Client.PutObjectAsync(request);

				var fileUrl = GetFileUrl(fileName, subPath);
				Logger.LogInformation("File uploaded to AWS S3: {Key}", key);
				return FileOperationResult.SuccessResult(key, fileUrl);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error uploading file to AWS S3: {FileName}", fileName);
				return FileOperationResult.FailureResult($"Failed to upload to AWS S3: {ex.Message}", ex);
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
				var key = BuildFilePath(fileName, subPath);
				var request = new Amazon.S3.Model.GetObjectRequest
				{
					BucketName = _config.BucketName,
					Key = key
				};

				var response = await _s3Client.GetObjectAsync(request);
				return response.ResponseStream;
			}
			catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return null;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error downloading file from AWS S3: {FileName}", fileName);
				return null;
			}
		}

		public override async Task<bool> DeleteFileAsync(string fileName, string? subPath = null)
		{
			try
			{
				var key = BuildFilePath(fileName, subPath);
				var request = new Amazon.S3.Model.DeleteObjectRequest
				{
					BucketName = _config.BucketName,
					Key = key
				};

				await _s3Client.DeleteObjectAsync(request);
				Logger.LogInformation("File deleted from AWS S3: {Key}", key);
				return true;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error deleting file from AWS S3: {FileName}", fileName);
				return false;
			}
		}

		public override async Task<bool> FileExistsAsync(string fileName, string? subPath = null)
		{
			try
			{
				var key = BuildFilePath(fileName, subPath);
				var request = new GetObjectMetadataRequest
				{
					BucketName = _config.BucketName,
					Key = key
				};

				await _s3Client.GetObjectMetadataAsync(request);
				return true;
			}
			catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return false;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error checking file existence in AWS S3: {FileName}", fileName);
				return false;
			}
		}

		public override string GetFileUrl(string fileName, string? subPath = null)
		{
			var key = BuildFilePath(fileName, subPath);
			return $"https://{_config.BucketName}.s3.{_config.Region}.amazonaws.com/{key}";
		}

		private string GetContentType(string fileName)
		{
			var extension = Path.GetExtension(fileName).ToLowerInvariant();
			return extension switch
			{
				".jpg" or ".jpeg" => "image/jpeg",
				".png" => "image/png",
				".gif" => "image/gif",
				".pdf" => "application/pdf",
				".txt" => "text/plain",
				".json" => "application/json",
				".xml" => "application/xml",
				_ => "application/octet-stream"
			};
		}
	}
}
