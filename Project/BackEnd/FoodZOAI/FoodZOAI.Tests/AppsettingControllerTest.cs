using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using Xunit;
using FoodZOAI.UserManagement.Services.Contract;
using FoodZOAI.UserManagement.Controllers;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.Tests
{
	public class AppsettingControllerTest
	{
		private readonly Mock<IAppsettingsService> _mockAppsettingsService;
		private readonly AppsettingController _appsettingController;

		public AppsettingControllerTest()
		{
			_mockAppsettingsService = new Mock<IAppsettingsService>();
			_appsettingController = new AppsettingController(_mockAppsettingsService.Object);
		}


		[Fact]
		public async Task GetAll_ReturnsOkResult_WithListOfAppSettings()
		{
			// Arrange
			var appSettings = new List<AppsettingDTO>
			{
				new() {
					Id=1,
					IsActive = true,
					CreatedByUser = "Rama",
					DeletedByUser = "Rama",
					Key = "Tests",
					ModifiedByUser ="Rama",
					Name = "Test",
					Value = "Test 1",
				},
				new() {
					Id=1,
					IsActive = true,
					CreatedByUser = "Rama",
					DeletedByUser = "Rama",
					Key = "Tests",
					ModifiedByUser ="Rama",
					Name = "Test",
					Value = "Test 2",
				}
			};
			_mockAppsettingsService.Setup(
				s=> s.GetAllAppsettingsAsync())
				.ReturnsAsync(appSettings);

			//Act
			var result = _appsettingController.GetAllAppSettingsAsync();

			//Assert
			//result.Result.Should().BeOfType<OkObjectResult>();
			var okResult = result.Result;// as OkObjectResult;
			okResult!.Value.Should().BeEquivalentTo(appSettings);
			_mockAppsettingsService.Verify(s => s.GetAllAppsettingsAsync(), Times.Once);
		}
	}
}
