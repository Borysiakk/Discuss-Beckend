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
        private ApplicationDbContext _dbContext;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions ;

        private void InitDbDData()
        {
            var users = new List<User>()
            {
                new User{ Id = 0, Email = "1@gmail.com", HashPass = "1234", Login = "LoginA"},
                new User{ Id = 0, Email = "2@gmail.com", HashPass = "1234", Login = "LoginB"},
                new User{ Id = 0, Email = "3@gmail.com", HashPass = "1234", Login = "LoginC"},
            };
            
            _dbContext.AddRange(users);
            _dbContext.SaveChanges();
        }
        
        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Users").Options;
            _dbContext = new ApplicationDbContext(_dbContextOptions);
            
            InitDbDData();
        }

        [Test]
        public void AddUser_OnAddUserIsCalled()
        {
            var userService = new UserService(_dbContext);
            var controller = new  UserController(userService);
        }

        [Test]
        public async Task GetUsers_OnGetUsersIsCalled()
        {
            var userService = new UserService(_dbContext);
            var controller = new  UserController(userService);

            var result = await controller.GetUsers();
            
        }
        
        
    }
}