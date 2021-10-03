using Discuss.Domain.Models.Entities;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuss.Test
{
    public class UnitTestUserService
    {
        private UserService _userService;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "UnitTestUserService").Options;

        private void InitDbDData()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = "0", Email = "1@gmail.com", Login = "LoginA"}
            };

            dbContext.AddRange(users);
            dbContext.SaveChanges();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _userService = new UserService(new ApplicationDbContext(_dbContextOptions));
            InitDbDData();
        }

        [Order(0)]
        [TestCase("LoginA")]
        public async Task AddClientCommunicationHubId(string login)
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var connectiongUser = await _userService.GetUserByLoginAsync(login);
            bool added = await _userService.AddClientCommunicationHubIdAsync(connectiongUser, "SignalRId");

            var modifiedUser = await _userService.GetUserByLoginAsync(login);

            Assert.True(added);
            Assert.True(!String.IsNullOrEmpty(modifiedUser.ClientCommunicationHubId));
            Assert.True(modifiedUser.ClientCommunicationHubId == "SignalRId");
        }

        [Order(1)]
        [TestCase("LoginA")]
        public async Task RemoveClientCommunicationHubId(string login)
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var connectingUser = await _userService.GetUserByLoginAsync(login);
            bool removed = await _userService.RemoveClientCommunicationHubIdAsync(connectingUser);

            var modifiedUser = await _userService.GetUserByLoginAsync(login);

            Assert.True(removed);
            Assert.True(String.IsNullOrEmpty(modifiedUser.ClientCommunicationHubId));
        }
    }
}
