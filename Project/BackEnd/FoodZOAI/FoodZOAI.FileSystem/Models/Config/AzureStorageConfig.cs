using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Models.Config
{
	public class AzureStorageConfig
	{
		public string ConnectionString { get; set; } = string.Empty;
		public string ContainerName { get; set; } = "files";
	}
}
