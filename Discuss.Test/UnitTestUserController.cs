using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Api.Controllers;
using Discuss.Domain.Models.Entities;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Discuss.Test
{
    [TestFixture]
    public class UnitTestUserController
    {
        private UserService _userService;
        private UserController _userController;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "UnitTestUserController").Options;

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
        
        [OneTimeSetUp]
        public void Setup()
        {
            _userService = new UserService(new ApplicationDbContext(_dbContextOptions));
            _userController = new UserController(_userService);
            
            InitDbDData();
        }
        
        [Order(0)]
        [TestCase("1@gmail.com")]
        [TestCase("2@gmail.com")]
        [TestCase("3@gmail.com")]
        public async Task GetUserByEmail_FindUserByEmail_User(string email)
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            
            var user = await _userController.GetUserByEmail(email);
            var okResult = user as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<User>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Order(1)]
        [TestCase("LoginA")]
        [TestCase("Login")]
        [TestCase("Log")]
        public async Task GetUsersByLogin_User(string login)
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            
            var actionResult = await _userController.GetUsersByLogin(login);
            var okResult = actionResult as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<IEnumerable<User>>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsTrue((okResult.Value as IEnumerable<User>).Count() > 0);
        }
        
        [Test,Order(2)]
        public async Task GetUsers_User()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            
            var actionResult = await _userController.GetUsers();
            var okResult = actionResult as ObjectResult;

            Assert.IsNotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<IEnumerable<User>>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsTrue((okResult.Value as IEnumerable<User>).Count() > 0);
        }

        [Order(3)]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("2")]
        public async Task DeleteUser_User(string id)
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            
            var deleteUserAction = await _userController.DeleteUser(id);
            var currentUsersAction = await _userController.GetUsers();
            
            var okResultDelete = deleteUserAction as ObjectResult;
            var okResultCurrentUsers = currentUsersAction as ObjectResult;
            var currentUser = okResultCurrentUsers.Value as IEnumerable<User>;
            
            Assert.IsNotNull(okResultDelete);
            Assert.True(okResultDelete is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResultDelete.StatusCode);
            
            Assert.IsNotNull(okResultCurrentUsers);
            Assert.True(okResultCurrentUsers is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResultCurrentUsers.StatusCode);
            
            Assert.True(currentUser is IEnumerable<User>);
            Assert.False(currentUser.Any(a=>a.Id == id));
            
        }
    }
}