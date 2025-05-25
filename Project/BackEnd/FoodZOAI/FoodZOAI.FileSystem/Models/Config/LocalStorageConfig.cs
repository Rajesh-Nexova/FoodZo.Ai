using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.FileSystem.Models.Config
{
	public class LocalStorageConfig
	{
		public string BasePath { get; set; } = "./uploads";
		public bool CreateDirectoryIfNotExists { get; set; } = true;
	}
}
