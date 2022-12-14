using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Exceptions;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers
{
    /**
     * <summary>Handle the logic about questions.</summary>
     */
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionsController : WithDbControllerBase
    {
#pragma warning disable CS1591
        public QuestionsController(IUserService userService, DatabaseContext dbContext)
            : base(userService, dbContext)
        {
        }
#pragma warning restore CS1591
        /**
         * <summary>获取目前用户提问的问题列表。</summary>
         * <remarks>该方法供提问者使用。没有问过问题的用户，例如助教或审计，调用此方法将获得空列表。</remarks>
         */
        [HttpGet("MyAsk")]
        public async Task<ActionResult<IList<Question>>> GetMyAsk()
        {
            var username = UserService.GetName(this);
            return await DbContext.Questions
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
            var username = UserService.GetName(this);
            return await DbContext.Questions.Where(q => q.Solver!.Username == username).ToListAsync();
        }

        /**
         * <summary>获取待解决的问题列表。</summary>
         * <remarks>该方法主要供助教使用。</remarks>
         */
        [HttpGet("Unsolved")]
        public async Task<ActionResult<IList<Question>>> GetUnsolved()
        {
            return await DbContext.Questions.Where(q => q.Status == QuestionStatus.Waiting).ToListAsync();
        }

        /**
         * <summary>获取系统中的所有问题。</summary>
         * <remarks>该方法供审计人员使用。其他人调用此方法将返回未授权错误。</remarks>
         */
        [HttpGet("All")]
        public async Task<ActionResult<IList<Question>>> GetAll()
        {
            return await DbContext.Questions.ToListAsync();
        }
        /**
         * <summary>获取某个问题。</summary>
         * <param name="id">问题ID</param>
         */
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Question>> Get(long id)
        {
            return await DbContext.Questions.SingleAsync(q => q.Id == id);
        }

        /**
         * <summary>提出一个新问题。</summary>
         * <remarks>尽管问题一般由学生提出，其他人员有问题时也可以调用此方法提问。</remarks>
         */
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] PostQuestionBody question)
        {
            var user = await UserService.GetUser(this);
            var newQuestion = new QuestionFactory().CreateQuestion(question, user);
            DbContext.Questions.Add(newQuestion);
            await DbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newQuestion.Id }, newQuestion);
        }

        /**
         * <summary>接下一个问题。</summary>
         * <remarks>助教调用此方法接下问题。</remarks>
         * <returns>与此问题对应的聊天会话。</returns>
         */
        [HttpPost("{id:long}/Take")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ChatSession>> TakeQuestion(long id)
        {
            var (question, user) = await GetQuestionsAndUser(id);
            if (question.Status != QuestionStatus.Waiting)
                throw new Http409ConflictException($"The status of question isn't \"Waiting\".");
            question.Status = QuestionStatus.Solving;
            question.Solver = user;
            question.Session = new ChatSession() { Id = 0, Participants = new List<User> { user, question.Sender } };
            await DbContext.SaveChangesAsync();
            return question.Session;
        }

        /**
         * <summary>将某个问题标记为已解决。</summary>
         */
        [HttpPost("{id:long}/Solve")]
        public async Task SolvedQuestion(long id)
        {
            var (question, user) = await GetQuestionsAndUser(id);
            if (question.Sender != user && question.Solver != user) throw new Http403ForbiddenException("Current user doesn't have enough permission to solve this question.");
            if (question.Status != QuestionStatus.Solving)
                throw new Http409ConflictException($"The status of question isn't \"Solving\"");
            question.Status = QuestionStatus.Solved;
            await DbContext.SaveChangesAsync();
        }

        private async Task<(Question, User)> GetQuestionsAndUser(long id)
        {
            var question = await DbContext.Questions.SingleAsync(q => q.Id == id);
            var user = await UserService.GetUser(this);
            return (question, user);
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
