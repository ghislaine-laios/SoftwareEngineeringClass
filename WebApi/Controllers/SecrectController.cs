using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
