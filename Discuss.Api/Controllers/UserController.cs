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
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/User/GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = (await userService.GetAllAsync()).ToList();
                
            if(!users.Any())
            {
                return users;
            }
            else return NotFound();
        }

        [HttpGet("/User/GetByLogin/{login}")]
        public ActionResult<IEnumerable<User>> GetUsersByLogin(string login)
        {
            var users = userService.GetUsersByLoginAsync(login).ToList();
            
            if (users != null && !users.Any())
            {
                return users;
            }
            else
                return NotFound();
        }

        [HttpGet("/User/GetByEmail/{email}")]
        public  ActionResult<User> GetUserByEmail(string email)
        {
            var users =  userService.GetUserByEmailAsync(email);
            if (users != null)
            {
                return users;
            }
            else return NotFound();
        }

        [HttpGet("/User/GetById/{id}")]
        public async Task<ActionResult<User>> GetUserById(long id)
        {
            var user = await userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("/User/Add")]
        public async Task<ActionResult<User>>AddUser(User user)
        {
            var retUser = await userService.AddAsync(user);
            if (retUser == null)
                return StatusCode(500, "Error during user adding.");
            else
                return Ok(retUser);
        }

        [HttpPut("/User/Update")]
        public async Task<ActionResult<User>>UpdateUser(User user)
        {
            var retUser = await userService.UpdateAsync(user);
            if (retUser == null)
                return StatusCode(500, "Error during user update.");
            else
                return Ok(retUser);
        }

        [HttpDelete("/User/DeleteUser/{id}")]
        public async Task<ActionResult<User>>DeleteUser(long id)
        {
            var retUser = await userService.DeleteAsync(id);
            if (retUser == null)
                return StatusCode(500, "Error during user delete.");
            else
                return Ok(retUser);
        }
    }
}
