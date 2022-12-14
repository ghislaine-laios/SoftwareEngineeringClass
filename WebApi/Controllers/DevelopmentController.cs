using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Exceptions;
using WebApi.Filters;
using WebApi.Model;

namespace WebApi.Controllers
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

        /**
         * <summary>触发409异常。</summary>
         */
        [HttpPost("Exception/409Conflict")]
        public void Trigger409Conflict()
        {
            throw new Http409ConflictException($"Demo Exception. Current action: {nameof(Trigger409Conflict)}");
        }

        [HttpPost("DebugDbContext")]
        public async Task DebugDbContext(DatabaseContext context)
        {
            var user = await context.Users.SingleAsync(user => user.Username == "user-12");
            var c1 = await context.ChatSessions.Include(cs => cs.Participants).Where(cs => cs.Participants.Contains(user)).ToListAsync();
            var now = DateTime.UtcNow;
            return;
        }
    }
}
