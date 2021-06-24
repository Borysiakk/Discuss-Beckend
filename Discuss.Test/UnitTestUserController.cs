using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Api.Controllers;
using Discuss.Domain.Models;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Discuss.Test
{
    public class UnitTestUserController
    {
        private UserService _userService;
        private UserController _userController;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PrimeDb").Options;

        private void InitDbDData()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = 0, Email = "1@gmail.com", HashPass = "1234", Login = "LoginA"},
                new User{ Id = 0, Email = "2@gmail.com", HashPass = "1234", Login = "LoginB"},
                new User{ Id = 0, Email = "3@gmail.com", HashPass = "1234", Login = "LoginC"},
            };
            
            dbContext.AddRange(users);
            dbContext.SaveChanges();
        }
        
        [SetUp]
        public void Setup()
        {
            _userService = new UserService(new ApplicationDbContext(_dbContextOptions));
            _userController = new UserController(_userService);
            
            InitDbDData();
        }
        
        [TestCase("1@gmail.com")]
        [TestCase("2@gmail.com")]
        [TestCase("3@gmail.com")]
        public async Task GetUserByEmail_FindUserByEmail_User(string email)
        {
            var user = await _userController.GetUserByEmail(email);
            
            Assert.IsNotNull(user.Value);
        }

        [TestCase("LoginA")]
        [TestCase("Login")]
        [TestCase("Log")]
        public async Task GetUsersByLogin_User(string login)
        {
            var actionResult = await _userController.GetUsersByLogin(login);

            //Assert.IsNotNull(actionResult.Value);
            Assert.IsTrue(actionResult.Value.Count() > 0);
        }
        
        [Test]
        public async Task GetUsers_User()
        {
            var actionResult = await _userController.GetUsers();

            Assert.IsNotNull(actionResult.Value);
        }
    }
}