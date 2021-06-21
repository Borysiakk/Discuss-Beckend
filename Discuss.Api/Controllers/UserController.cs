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
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return userData.GetAllUsers().ToList();
        }


    }
}
