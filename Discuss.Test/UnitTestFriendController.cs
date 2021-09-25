using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Domain.Models.Contract.Result;
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
        private Dictionary<string, IEnumerable<string>> _friendly;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "UnitTestFriendController").Options;
        
        private void InitDb()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = "0", Email = "1@gmail.com", Login = "LoginA", Status = true},
                new User{ Id = "1", Email = "2@gmail.com", Login = "LoginB", Status = false},
                new User{ Id = "2", Email = "3@gmail.com", Login = "LoginC", Status = false},
                new User{ Id = "3", Email = "3@gmail.com", Login = "LoginC", Status = false},
                new User{ Id = "4", Email = "3@gmail.com", Login = "LoginC", Status = false},
            };

            var friends = new List<Friend>()
            {
                new Friend() {UserId = "0",FriendlyId = "1"},
                new Friend() {UserId = "1",FriendlyId = "0"},
                new Friend() {UserId = "1",FriendlyId = "2"},
                new Friend() {UserId = "2",FriendlyId = "1"},
            };

            _friendly = new Dictionary<string, IEnumerable<string>>()
            {
                {"0",new List<string>(){"1"}},
                {"1",new List<string>(){"0","2"}},
                {"2",new List<string>(){"1"}},
            };
            
            dbContext.AddRange(users);
            dbContext.AddRange(friends);

            dbContext.SaveChanges();
        }
        
        [OneTimeSetUp]
        public void Setup()
        {
            _friendService = new FriendService(new ApplicationDbContext(_dbContextOptions));
            
            InitDb();
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("2")]
        public async Task GetFriend_GetFriends_User(string id)
        {
            var friendly = _friendly[id];
            var users = _friendService.GetFriends(id);
            var usersId = users.Select(a => a.Id);
            
            Assert.NotNull(users,"users is null");
            Assert.NotNull(usersId,"usersId is null");
            Assert.NotNull(friendly,"friendly is null");
            
            Assert.IsInstanceOf<IEnumerable<User>>(users);
            Assert.IsInstanceOf<IEnumerable<string>>(usersId);
            Assert.IsInstanceOf<IEnumerable<string>>(friendly);
            
            Assert.AreEqual(usersId,friendly);
        }
        
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("2")]
        public async Task GetFriendsWithStatus_GetStatusFriends_Users(string id)
        {
            var friendly = _friendly[id];
            Assert.NotNull(friendly,"friendly is null");
            
            var usersWithStatus = _friendService.GetFriendsWithStatus(id);
            Assert.NotNull(usersWithStatus,"users is null");

            var usersId = usersWithStatus.Keys;
            Assert.NotNull(usersId,"usersId is null");
            
            Assert.IsInstanceOf<IEnumerable<string>>(usersId);
            Assert.IsInstanceOf<IEnumerable<string>>(friendly);
            Assert.IsInstanceOf<Dictionary<string,bool>>(usersWithStatus);
            
            Assert.AreEqual(usersId,friendly);

        }
        
        [TestCase("3","4")]
        [TestCase("4","3")]
        public async Task AddFriend_Add_FriendResult(string userId, string friendId)
        {
            var resultAddFriend = await _friendService.Add(userId, friendId);
            
            Assert.NotNull(resultAddFriend,"users is null");
            Assert.IsInstanceOf<FriendResult>(resultAddFriend);
            Assert.True(resultAddFriend.Succeeded);
        }
        
        [TestCase("0","1")]
        [TestCase("2","1")]
        public async Task AddExistingFriend_Add_FriendResult(string userId, string friendId)
        {
            var resultAddFriend = await _friendService.Add(userId, friendId);
            
            Assert.NotNull(resultAddFriend,"users is null");
            Assert.IsInstanceOf<FriendResult>(resultAddFriend);
            Assert.False(resultAddFriend.Succeeded);
        }
    }
}