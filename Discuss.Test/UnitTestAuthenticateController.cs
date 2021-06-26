using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discuss.Api.Controllers;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract;
using Discuss.Domain.Models.Entities;
using Discuss.Infrastructure.Services;
using Discuss.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Discuss.Test
{
    public class UnitTestAuthenticateController
    {
        private UserService _userService;
        private JwtTokenService _jwtTokenService;
        private AuthenticateService _authenticateService;
        private AuthenticateController _authenticateController;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "PrimeDb").Options;

        private void InitDb()
        {
            using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var users = new List<User>()
            {
                new User{  Email = "1@gmail.com", Login = "loginA", PasswordSalt = "$2a$11$yhL3EIw5oIs2TlIypUNbRe", PasswordHash = "$2a$11$RSoUXc3Ux6h0ne9gTvvoEOQ7eyBZrrKVjgJZ4GCvQJHlex.hzyogS"},
                new User{  Email = "2@gmail.com", Login = "loginB", PasswordSalt = "$2a$11$5DeopQxewvwvLJh3j8w8LO", PasswordHash = "$2a$11$5DeopQxewvwvLJh3j8w8LOV8BcjYzAR5XLYhvrLfAdPvDX7nFAyrm"},
                new User{  Email = "3@gmail.com", Login = "LoginC", PasswordSalt = "$2a$11$N7/IguGspopsbCwR6t6HI.", PasswordHash = "$2a$11$N7/IguGspopsbCwR6t6HI.Ebk/2kkiq.cvhNYwVlhL/RnXwEz0LU6"},
            };
            
            dbContext.AddRange(users);
            dbContext.SaveChanges();
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings-test.json").Build();
            return config;
        }
        
        [SetUp]
        public void Setup()
        {
            var configuration = InitConfiguration();

            _userService = new UserService(new ApplicationDbContext(_dbContextOptions));
            _jwtTokenService = new JwtTokenService(configuration);
            _authenticateService = new AuthenticateService(_userService, _jwtTokenService);
            _authenticateController = new AuthenticateController(_authenticateService);
            
            InitDb();
        }

        [TestCase("loginA","string")]
        [TestCase("loginB","string")]
        public async Task Login_LoginCheck_AuthenticateResult(string login,string password)
        {
            var result = await _authenticateController.Login(new LoginModelView() {Login = login, Password = password});
            var okResult = result as ObjectResult;
            
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsInstanceOf<AuthenticateResult>(okResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }
        
        [TestCase("loginA","string1")]
        [TestCase("loginB","string1")]
        public async Task Login_InvalidLogin_AuthenticateResult(string login,string password)
        {
            var result = await _authenticateController.Login(new LoginModelView() {Login = login, Password = password});
            var unauthorizedResult = result as ObjectResult;
            
            Assert.NotNull(unauthorizedResult);
            Assert.True(unauthorizedResult is UnauthorizedObjectResult);
            Assert.IsInstanceOf<AuthenticateResult>(unauthorizedResult.Value);
            Assert.AreEqual(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }
    }
}