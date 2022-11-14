using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Filters;
using WebApiPlayground.Model;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    /**
     * <summary>Handle the logic about chat.</summary>
     */
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsController : WithDbControllerBase
    {
#pragma warning disable CS1591
        public ChatsController(IUserService userService, DatabaseContext dbContext) 
            : base(userService, dbContext)
        {
        }
#pragma warning restore CS1591
    }
}
