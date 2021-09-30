using System;
using System.Threading.Tasks;
using Discuss.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discuss.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : Controller
    {
        private readonly FriendService _friendService;

        public FriendController(FriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string userId,string userFriendId)
        {
            try
            {
                var result = await _friendService.AddAsync(userId, userFriendId);
                if (!result.Succeeded)
                {
                    return new ConflictObjectResult(result);
                }

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}