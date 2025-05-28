using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Azure.Storage.Blobs;
using Amazon.S3;
using Amazon.S3.Model;
namespace FoodZOAI.FileSystem.Models.Config
{
	public class StorageConfiguration
	{
		public string Provider { get; set; } = "Local"; // Local, Azure, AWS
		public AzureStorageConfig? Azure { get; set; }
		public AwsStorageConfig? Aws { get; set; }
		public LocalStorageConfig? Local { get; set; }
	}
}
