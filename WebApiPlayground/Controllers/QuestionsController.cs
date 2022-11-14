using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Exceptions;
using WebApiPlayground.Filters;
using WebApiPlayground.Services;
using WebApiPlayground.Model;
using static WebApiPlayground.Controllers.QuestionsController;

namespace WebApiPlayground.Controllers
{
    /**
     * <summary>Handle the logic about questions.</summary>
     */
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Transactional]
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
         * <summary>获取待解决的问题列表。</summary>
         * <remarks>该方法主要供助教使用。</remarks>
         */
        [HttpGet("Unsolved")]
        public async Task<ActionResult<IList<Question>>> GetUnsolved()
        {
            return await _dbContext.Questions.Where(q => q.Status == Question.QuestionStatus.Waiting).ToListAsync();
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] PostQuestionBody question)
        {
            var username = _userService.GetName(this);
            var user = await _dbContext.Users.SingleAsync(u => u.Username == username);
            var newQuestion = new QuestionFactory().CreateQuestion(question, user);
            _dbContext.Questions.Add(newQuestion);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id=newQuestion.Id}, newQuestion);
        }

        /**
         * <summary>接下一个问题。</summary>
         * <remarks>助教调用此方法接下问题。</remarks>
         * <returns>与此问题对应的聊天会话。</returns>
         */
        [HttpPost("{id:long}/Take")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ChatSession>> TakeQuestion(long id)
        {
            var question = await _dbContext.Questions.SingleAsync(q =>q.Id == id);
            var username = _userService.GetName(this);
            var user = await _dbContext.Users.SingleAsync(u => u.Username == username);
            if (question.Status != Question.QuestionStatus.Waiting)
                throw new Http409ConflictException($"The status of question isn't \"Waiting\".");
            question.Status = Question.QuestionStatus.Solving;
            question.Solver = user;
            question.Session = new ChatSession() { Id = 0, Participants = new List<User>{ user, question.Sender } };
            await _dbContext.SaveChangesAsync();
            return question.Session;
        }
    }

    public class PostQuestionBody
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    internal static class PostQuestionBodyExtension
    {
        public static Question CreateQuestion(this QuestionFactory factory, PostQuestionBody questionBody, User sender)
        {
            return new Question()
                { Title = questionBody.Title, Description = questionBody.Description, Id = 0, Sender = sender };
        }
    }
}
