using Discuss.Domain;
using Discuss.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discuss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        InMemoryUserData userData; //Tylko dla celów testowych
        public UserController()
        {
            userData = new InMemoryUserData();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await userData.GetAllAsync();
            if(users!=null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByLogin(string login)
        {
            var users = await userData.GetUsersByLoginAsync(login);
            if (users != null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByEmail(string email)
        {
            var users = await userData.GetUsersByEmailAsync(email);
            if (users != null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }
    }
}
