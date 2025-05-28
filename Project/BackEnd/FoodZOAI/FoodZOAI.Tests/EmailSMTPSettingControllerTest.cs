using FluentAssertions;
using FoodZOAI.UserManagement.Controllers;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodZOAI.Tests
{
   public  class EmailSMTPSettingControllerTest
    {
        private readonly Mock<IEmailSMTPSettingService> _mockEmailSMTPSettingService;
        private readonly EmailSMTPSettingController _emailsmtpContoller;


        public EmailSMTPSettingControllerTest()
        {
            _mockEmailSMTPSettingService = new Mock<IEmailSMTPSettingService>();
            _emailsmtpContoller = new EmailSMTPSettingController(_mockEmailSMTPSettingService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfEmailSMTPSettings()
        {
            var emailsettings = new List<EmailSettingDTO>
        {
            new()
            {
                Id = 1,
                Host = "Admin",
                Port = 567,
                UserName = "lavanya",
                Password = "1234",
                IsEnableSsl = true,
                IsDefault = true,
                CreatedByUser = "lavanya",
                ModifiedByUser  = "lavanya",
                DeletedByUser = "lavanya",
            },
            new()
            {
                 Id = 1,
                Host = "Admin",
                Port = 567,
                UserName = "lavanya",
                Password = "1234",
                IsEnableSsl = true,
                IsDefault = true,
                CreatedByUser = "lavanya ",
                ModifiedByUser  = "lavanya",
                DeletedByUser = "lavanya",
            }
        };
            _mockEmailSMTPSettingService.Setup(
                s => s.GetAllEmailSMTPSettingAsync())
                .ReturnsAsync(emailsettings);

            var result = _emailsmtpContoller.GetAllAsync();

            var okresult = result.Result;
            okresult!.Value.Should().BeEquivalentTo(emailsettings);
            _mockEmailSMTPSettingService.Verify(s => s.GetAllEmailSMTPSettingAsync(), Times.Once);
        }
    }
}
