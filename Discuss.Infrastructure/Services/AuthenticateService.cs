using System;
using System.Net;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;

using BC = BCrypt.Net.BCrypt;

namespace Discuss.Infrastructure.Services
{
    public class AuthenticateService :IAuthenticateService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticateService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public Task<AuthenticateResult> LoginAsync(LoginModelView loginModelView)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthenticateResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            User user = new User()
            {
                Login = registerViewModel.Login,
                Email = registerViewModel.Password,
            };

            var resultCreateUser = await _userService.CreateAsync(user, registerViewModel.Password);
            if (!resultCreateUser.Succeeded)
            {
                return new AuthenticateResult()
                {
                    Errors = resultCreateUser.Errors,
                    StatusCode = resultCreateUser.StatusCode,
                };
            }

            return new AuthenticateResult()
            {
                AuthDate = DateTime.Now,
                StatusCode = resultCreateUser.StatusCode,
                Token = _tokenService.Generate(resultCreateUser.User),
            };
        }
    }
}