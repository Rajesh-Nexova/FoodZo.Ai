using FoodZOAI.FileSystem.Core.Contract;

namespace FoodZOAI.UserManagement.FileFactory
{
	public interface IFileStorageFactory
	{
		IFileStorageService CreateStorageService();
	}
}
