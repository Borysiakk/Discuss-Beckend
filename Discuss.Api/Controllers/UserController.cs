using Discuss.Domain;
using Discuss.Domain.Interfaces;
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
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await userService.GetAllAsync();
            if(users!=null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }

        [HttpGet("/GetByLogin/{login}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByLogin(string login)
        {
            var users = await userService.GetUsersByLoginAsync(login);
            if (users != null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }

        [HttpGet("/GetByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByEmail(string email)
        {
            var users = await userService.GetUsersByEmailAsync(email);
            if (users != null)
            {
                return users.ToList();
            }
            else
                return StatusCode(400, "Error during user retrieve.");
        }

        [HttpGet("/GetById/{id}")]
        public async Task<ActionResult<User>> GetUserById(long id)
        {
            var user = await userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("/AddUser")]
        public async Task<ActionResult<User>>AddUser(User user)
        {
            var retUser = await userService.AddAsync(user);
            if (retUser == null)
                return StatusCode(500, "Error during user adding.");
            else
                return retUser;
        }

        [HttpPut("/UpdateUser")]
        public async Task<ActionResult<User>>UpdateUser(User user)
        {
            var retUser = await userService.UpdateAsync(user);
            if (retUser == null)
                return StatusCode(500, "Error during user update.");
            else
                return retUser;
        }

        [HttpDelete("/DeleteUser/{id}")]
        public async Task<ActionResult<User>>DeleteUser(long id)
        {
            var retUser = await userService.DeleteAsync(id);
            if (retUser == null)
                return StatusCode(500, "Error during user delete.");
            else
                return retUser;
        }
    }
}
