using Discuss.SignalR.Hubs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NLog.Web;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.SignalR.Models;
using Microsoft.EntityFrameworkCore;
using Discuss.Persistence;
using Discuss.Infrastructure.Services;
using Discuss.Domain.Models.Entities;

namespace Discuss.Test
{
    [TestFixture]
    public class UnitTestLogger
    {
        private UserService _userService;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "UnitTestLogger").Options;
        Mock<ILogger<CommunicationHub>> _loggerMock;
        CommunicationHub _hub;

        [OneTimeSetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CommunicationHub>>();
            _userService = new UserService(new ApplicationDbContext(_dbContextOptions));
            _hub = new CommunicationHub(_loggerMock.Object, _userService);
        }

        private void InitDbDData()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = "0", Email = "1@gmail.com", Login = "LoginA"},
                new User{ Id = "1", Email = "2@gmail.com", Login = "LoginB"},
                new User{ Id = "2", Email = "3@gmail.com", Login = "LoginC"},
            };

            dbContext.AddRange(users);
            dbContext.SaveChanges();
        }

        [TestCase("1 Test run")]
        public async Task LogErrorMessage(string info)
        {
            var msg = new MessageData() { Date = DateTime.Now, Message = "Test", DestinationClientId="cl1", SendingClientId="cl2", MessageId="000-000-000" };
            await _hub.SendMessageToClient(msg);

            _loggerMock.Verify(
                m => m.Log(LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Error occured")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }
    }
}
