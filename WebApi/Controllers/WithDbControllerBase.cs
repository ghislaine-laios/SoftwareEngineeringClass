using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers
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
        public DatabaseContext DbContext { get; }

        public WithDbControllerBase(IUserService userService, DatabaseContext dbContext)
        {
            UserService = userService;
            DbContext = dbContext;
        }
#pragma warning restore CS1591
    }
}
