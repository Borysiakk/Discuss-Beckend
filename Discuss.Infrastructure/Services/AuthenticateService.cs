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

        public async Task<AuthenticateResult> LoginAsync(LoginModelView loginModelView)
        {
            var resultFindLogin = await _userService.GetUserByLoginAsync(loginModelView.Login);
            if (resultFindLogin == null)
            {
                return new AuthenticateResult()
                {
                    Succeeded = false,
                    Errors = new[] {"User not found"},
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            var resultCheckPassword = BC.Verify(loginModelView.Password, resultFindLogin.PasswordHash);
            if (!resultCheckPassword)
            {
                return new AuthenticateResult()
                {
                    Succeeded = false,
                    Errors = new[] {"User not found"},
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            return new AuthenticateResult()
            {
                Succeeded = true,
                AuthDate = DateTime.Now,
                StatusCode = HttpStatusCode.OK,
                Token = _tokenService.Generate(resultFindLogin),
            };
        }

        public async Task<AuthenticateResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            User user = new User()
            {
                Login = registerViewModel.Login,
                Email = registerViewModel.Email,
            };

            var resultCreateUser = await _userService.CreateAsync(user, registerViewModel.Password);
            if (!resultCreateUser.Succeeded)
            {
                return new AuthenticateResult()
                {
                    Succeeded = false,
                    Errors = resultCreateUser.Errors,
                    StatusCode = resultCreateUser.StatusCode,
                };
            }

            return new AuthenticateResult()
            {
                Succeeded = true,
                AuthDate = DateTime.Now,
                StatusCode = resultCreateUser.StatusCode,
                Token = _tokenService.Generate(resultCreateUser.User),
            };
        }
    }
}