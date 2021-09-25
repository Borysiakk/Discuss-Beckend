using Discuss.Domain;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Domain.Models.Entities;

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetAllAsync();
                
            if(users != null)
            {
                return new OkObjectResult(users);
            }
            else return new EmptyResult();
        }

        [HttpGet("GetByLogin/{login}")]
        public async Task<IActionResult> GetUsersByLogin(string login)
        {
            var users = await userService.GetUsersByLoginAsync(login);
            
            if (users != null)
            {
                return new OkObjectResult(users);
            }
            else
                return NotFound();
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user =  await userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return new OkObjectResult(user);
        }

        // [HttpPost("/User/Add")]
        // public async Task<ActionResult<User>>AddUser(User user)
        // {
        //     //dodać dodawanie hasła
        //     var retUser = await userService.CreateAsync(user,"");
        //     if (retUser == null)
        //         return StatusCode(500, "Error during user adding.");
        //     else
        //         return retUser;
        // }

        [HttpPut("Update")]
        public async Task<IActionResult>UpdateUser(User user)
        {
            var retUser = await userService.UpdateAsync(user);
            if (retUser == null)
                return StatusCode(500, "Error during user update.");
            else
                return new OkObjectResult(retUser);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult>DeleteUser(string id)
        {
            var retUser = await userService.DeleteAsync(id);
            if (retUser == null)
                return StatusCode(500, "Error during user delete.");
            else
                return new OkObjectResult(retUser);
        }
    }
}
