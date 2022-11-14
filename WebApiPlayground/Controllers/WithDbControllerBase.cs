using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Filters;
using WebApiPlayground.Model;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    /**
     * <summary>The base controller class with dbContext support.</summary>
     */
    [ApiController]
    [Produces("application/json")]
    [Transactional]
    public class WithDbControllerBase : ControllerBase
    {
#pragma warning disable CS1591
        protected readonly IUserService UserService;
        protected readonly DatabaseContext DbContext;

        public WithDbControllerBase(IUserService userService, DatabaseContext dbContext)
        {
            UserService = userService;
            DbContext = dbContext;
        }
#pragma warning restore CS1591
    }
}
