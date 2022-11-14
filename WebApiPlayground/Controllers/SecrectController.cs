using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Filters;

namespace WebApiPlayground.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SecrectController : ControllerBase
    {
        [HttpGet("name")]
        public Object? EchoName()
        {
            return this.User.Identity?.Name;
        }
    }
}
