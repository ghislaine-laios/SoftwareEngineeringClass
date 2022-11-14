using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Services;
using WebApiPlayground.Model;

namespace WebApiPlayground.Controllers
{
    /**
     * <summary>Handle the logic about questions.</summary>
     */
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class QuestionsController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly DatabaseContext _dbContext;

#pragma warning disable CS1591
        public QuestionsController(IUserService userService, DatabaseContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

#pragma warning restore CS1591
        /**
         * <summary>获取目前用户提问的问题列表。</summary>
         * <remarks>该方法供提问者使用。没有问过问题的用户，例如助教或审计，调用此方法将获得空列表。</remarks>
         */
        [HttpGet("MyAsk")]
        public async Task<ActionResult<IList<Question>>> GetMyAsk()
        {
            var username = _userService.GetName(this);
            return await _dbContext.Questions
                .Where(q => q.Sender.Username == username)
                .ToListAsync();
        }

        /**
         * <summary>获取被目前用户解决的问题的列表。</summary>
         * <remarks>该方法供助教使用。没有解决过问题的用户调用此方法将获得空列表。</remarks>
         */
        [HttpGet("SolvedByMe")]
        public async Task<ActionResult<IList<Question>>> GetSolvedByMe()
        {
            var username = _userService.GetName(this);
            return await _dbContext.Questions.Where(q => q.Solver!.Username == username).ToListAsync();
        }

        /**
         * <summary>获取系统中的所有问题。</summary>
         * <remarks>该方法供审计人员使用。其他人调用此方法将返回未授权错误。</remarks>
         */
        [HttpGet("All")]
        public async Task<ActionResult<IList<Question>>> GetAll()
        {
            return await _dbContext.Questions.ToListAsync();
        }
        /**
         * <summary>获取某个问题。</summary>
         * <param name="id">问题ID</param>
         */
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Question>> Get(long id)
        {
            return await _dbContext.Questions.SingleAsync(q => q.Id == id);
        }

        /**
         * <summary>提出一个新问题。</summary>
         * <remarks>尽管问题一般由学生提出，其他人员有问题时也可以调用此方法提问。</remarks>
         */
        [HttpPost]
        public async Task<IActionResult> PostQuestion(Question question)
        {
            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
