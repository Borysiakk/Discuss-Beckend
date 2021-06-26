using System;
using System.Net;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Discuss.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class AuthenticateController :ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService, ILogger<AuthenticateController> logger)
        {
            _logger = logger;
            _authenticateService = authenticateService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticateResult>> Login(LoginModelView login)
        {
            try
            {
                var result = await _authenticateService.LoginAsync(login);
                if (result.Succeeded)
                {
                    return result;
                }
                else
                {
                    return Unauthorized(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Log(LogLevel.Warning, e,e.Message);
                throw;
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticateResult>> Register(RegisterViewModel register)
        {
            try
            {
                var result = await _authenticateService.RegisterAsync(register);
                if (result.Succeeded)
                {
                    return result;
                }
                else
                {
                    return BadRequest(result.Errors);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Log(LogLevel.Warning, e,e.Message);
                throw;
            }
        }
    }
}