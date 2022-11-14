using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Filters;
using WebApiPlayground.Model;

namespace WebApiPlayground.Controllers
{
    /**
     * <summary>提供开发中的便捷方法。</summary>
     */
    [Route("api/[controller]")]
    [ApiController]
    [DevOnly]
    public class DevelopmentController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

#pragma warning disable CS1591
        public DevelopmentController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
#pragma warning restore CS1591

        /**
         * <summary>使用demo数据填充数据库。填充前数据库将被清空。</summary>
         */
        [HttpPost("Seed")]
        public async Task<ActionResult> SeedDatabase()
        {
            await DatabaseContextFactory.Seed(_dbContext, true);
            return Ok();
        }
    }
}
