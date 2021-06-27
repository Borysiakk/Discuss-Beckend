using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Api.Controllers;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Entities;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Discuss.Test
{
    public class UnitTestUserController
    {
        private UserService _userService;
        private UserController _userController;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "PrimeDb").Options;

        private void InitDbDData()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{ Id = 0, Email = "1@gmail.com", Login = "LoginA"},
                new User{ Id = 0, Email = "2@gmail.com", Login = "LoginB"},
                new User{ Id = 0, Email = "3@gmail.com", Login = "LoginC"},
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
            var okResult = user as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<User>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestCase("LoginA")]
        [TestCase("Login")]
        [TestCase("Log")]
        public async Task GetUsersByLogin_User(string login)
        {
            var actionResult = await _userController.GetUsersByLogin(login);
            var okResult = actionResult as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<IEnumerable<User>>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsTrue((okResult.Value as IEnumerable<User>).Count() > 0);
        }
        
        [Test]
        public async Task GetUsers_User()
        {
            var actionResult = await _userController.GetUsers();
            var okResult = actionResult as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<IEnumerable<User>>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsTrue((okResult.Value as IEnumerable<User>).Count() > 0);
        }
    }
}