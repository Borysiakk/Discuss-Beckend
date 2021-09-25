using System.Collections.Generic;
using System.Threading.Tasks;
using Discuss.Domain.Models.Entities;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Discuss.Test
{
    [TestFixture]
    public class UnitTestFriendController
    {
        private FriendService _friendService;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "UnitTestFriendController").Options;

        private void InitDb()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = "0", Email = "1@gmail.com", Login = "LoginA"},
                new User{ Id = "1", Email = "2@gmail.com", Login = "LoginB"},
                new User{ Id = "2", Email = "3@gmail.com", Login = "LoginC"},
            };

            var friends = new List<Friend>()
            {
                new Friend() {UserId = "0",FriendlyId = "1"},
                new Friend() {UserId = "1",FriendlyId = "0"},
                new Friend() {UserId = "1",FriendlyId = "2"},
                new Friend() {UserId = "2",FriendlyId = "1"},
            };
            
            dbContext.AddRange(users);
            dbContext.AddRange(friends);

            dbContext.SaveChanges();
        }
        
        [SetUp]
        public void Setup()
        {
            _friendService = new FriendService(new ApplicationDbContext(_dbContextOptions));
            
            InitDb();
        }

        [TestCase("1")]
        public async Task GetFriend_FindFriend_User(string id)
        {
            var result =_friendService.GetFriends(id);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<User>>(result);
            
        }
    }
}