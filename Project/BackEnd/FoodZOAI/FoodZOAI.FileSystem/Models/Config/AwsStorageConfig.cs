using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Models.Config
{
	public class AwsStorageConfig
	{
		public string AccessKey { get; set; } = string.Empty;
		public string SecretKey { get; set; } = string.Empty;
		public string BucketName { get; set; } = "my-bucket";
		public string Region { get; set; } = "us-east-1";
	}
}
