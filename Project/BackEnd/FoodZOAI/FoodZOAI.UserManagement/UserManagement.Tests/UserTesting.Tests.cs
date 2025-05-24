using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FoodZOAI.UserManagement.Configuration.Contracts;

using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Controllers;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserTesting.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserProfileRepository> _userProfileRepositoryMock;
        private readonly Mock<IMapperServices<User, UserDTO>> _userMapperMock;
        private readonly Mock<IMapperServices<UserProfile, UserProfileDTO>> _profileMapperMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly UserController _controller;

        //public UserControllerTests()
        //{
        //    _userRepositoryMock = new Mock<IUserRepository>();
        //    _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
        //    _userMapperMock = new Mock<IMapperService<User, UserDTO>>();
        //    _profileMapperMock = new Mock<IMapperService<UserProfile, UserProfileDTO>>();
        //    _fileServiceMock = new Mock<IFileService>();

        //    _controller = new UserController(
        //        _userRepositoryMock.Object,
        //        _userProfileRepositoryMock.Object,
        //        _userMapperMock.Object,
        //        _profileMapperMock.Object,
        //        _fileServiceMock.Object
        //    );
        //}

        //[Fact]
        //public async Task AddUser_ValidUser_ReturnsOkResult()
        //{
        //    // Arrange
        //    var userDto = new UserDTO
        //    {
        //        Username = "johndoe",
        //        Email = "john@example.com",
        //        PasswordHash = "hashedpassword123",
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    var user = new User
        //    {
        //        Id = 1,
        //        Username = userDto.Username,
        //        Email = userDto.Email,
        //        PasswordHash = userDto.PasswordHash,
        //        CreatedAt = userDto.CreatedAt
        //    };

        //    _userMapperMock.Setup(m => m.MapToEntity(It.IsAny<UserDTO>())).Returns(user);
        //    _userRepositoryMock.Setup(r => r.AddUser(It.IsAny<User>())).ReturnsAsync(user);

        //    // Act
        //    var result = await _controller.AddUser(userDto);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedUser = Assert.IsType<User>(okResult.Value);
        //    Assert.Equal(user.Username, returnedUser.Username);
        //}

        //[Fact]
        //public async Task GetUserById_UserExists_ReturnsOkResult()
        //{
        //    // Arrange
        //    int userId = 1;
        //    var user = new User
        //    {
        //        Id = userId,
        //        Username = "testuser",
        //        Email = "test@example.com",
        //        PasswordHash = "secret",
        //        CreatedAt = DateTime.UtcNow
        //    };
        //    var userDto = new UserDTO
        //    {
        //        Username = user.Username,
        //        Email = user.Email,
        //        PasswordHash = user.PasswordHash,
        //        CreatedAt = (DateTime)user.CreatedAt
        //    };

        //    _userRepositoryMock.Setup(r => r.GetUserById(userId)).ReturnsAsync(user);
        //    _userMapperMock.Setup(m => m.MapToDTO(user)).Returns(userDto);

        //    // Act
        //    var result = await _controller.GetUserById(userId);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedDto = Assert.IsType<UserDTO>(okResult.Value);
        //    Assert.Equal(userDto.Email, returnedDto.Email);
        //}

        //[Fact]
        //public async Task GetUserById_UserNotFound_ReturnsNotFound()
        //{
        //    // Arrange
        //    int userId = 999;
        //    _userRepositoryMock.Setup(r => r.GetUserById(userId)).ReturnsAsync((User)null);

        //    // Act
        //    var result = await _controller.GetUserById(userId);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public async Task GetAllUsers_ReturnsListOfUsers()
        //{
        //    // Arrange
        //    var users = new List<User>
        //    {
        //        new User { Id = 1, Username = "user1", Email = "u1@example.com", PasswordHash = "p1", CreatedAt = DateTime.UtcNow },
        //        new User { Id = 2, Username = "user2", Email = "u2@example.com", PasswordHash = "p2", CreatedAt = DateTime.UtcNow }
        //    };

        //    var userDtos = new List<UserDTO>
        //    {
        //        new UserDTO { Username = "user1", Email = "u1@example.com", PasswordHash = "p1", CreatedAt = DateTime.UtcNow },
        //        new UserDTO { Username = "user2", Email = "u2@example.com", PasswordHash = "p2", CreatedAt = DateTime.UtcNow }
        //    };

        //    _userRepositoryMock.Setup(r => r.GetAllUsers()).ReturnsAsync(users);
        //    _userMapperMock.Setup(m => m.MapToDTOList(users)).Returns(userDtos);

        //    // Act
        //    var result = await _controller.GetAllUsers();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedList = Assert.IsType<List<UserDTO>>(okResult.Value);
        //    Assert.Equal(2, returnedList.Count);
        //}
    }
}
