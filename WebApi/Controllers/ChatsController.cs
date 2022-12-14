using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Exceptions;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers
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


        public class MessageResponse : MessageBase
        {
            public UserBase Sender { get; set; }

            public static MessageResponse From(Message message)
            {
                return new MessageResponse()
                {
                    // Hide the actual Id to prevent attacks.
                    Id = message.IdPerChat,
                    Content = message.Content,
                    SentTime = message.SentTime,
                    Sender = message.Sender
                };
            }
        }

        public class MessagePayload : MessageBase
        {

            public Message ToMessage(long idPerChat, long senderId)
            {
                return new Message()
                {
                    Content = this.Content,
                    SenderId = senderId,
                    SentTime = this.SentTime,
                    IdPerChat = idPerChat
                };
            }
        }

        public class ChatSessionResponse
        {
            public long Id { get; set; }
            public IList<UserBase> Participants { get; set; }
            public IList<MessageResponse> Messages { get; set; }

            public static ChatSessionResponse From(ChatSession session)
            {
                return new ChatSessionResponse()
                {
                    Id = session.Id,
                    Messages = session.Messages.Select(MessageResponse.From).ToList(),
                    Participants = session.Participants.Select(p => (UserBase)p).ToList()
                };
            }
        }
#pragma warning restore CS1591

        /**
         * <summary>获取目前用户所有的聊天会话。</summary>
         * <remarks>每条记录将会附上最新的五条聊天记录。</remarks>
         */
        [HttpGet("My")]
        public async Task<ActionResult<IList<ChatSessionResponse>>> GetChatSessions()
        {
            var username = UserService.GetName(this);
            var user = await DbContext.Users.Include(u => u.ChatSessions).SingleAsync(u => u.Username == username);

            var sessions = await DbContext.ChatSessions
                .Include(cs => cs.Participants)
                .Include(cs => cs.Messages.OrderByDescending(m => m.SentTime).Take(5))
                .Where(cs => user.ChatSessions.Contains(cs))
                .ToListAsync();

            return sessions.Select(ChatSessionResponse.From).ToList();
        }

        /**
         * <summary>获取某个聊天会话。</summary>
         * <remarks>返回的会话附带聊天记录。</remarks>
         */
        [HttpGet("{id:long}")]
        public async Task<ChatSessionResponse> GetChatSession(long id)
        {
            var user = await UserService.GetUser(this);
            var session = await DbContext.ChatSessions
                .Include(s => s.Participants)
                .Include(s => s.Messages.OrderByDescending(m => m.SentTime))
                .SingleAsync(cs => cs.Id == id);
            CheckSessionAuthority(session, user);
            return ChatSessionResponse.From(session);
        }

        /**
         * <summary>创建一条新信息。</summary>
         */
        [HttpPost("{id:long}/NewMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateMessage(long id, [FromBody] MessagePayload messagePayload)
        {
            CheckMessagePayload(messagePayload);
            var (user, chatSession) = await GetUserAndChatSession(id);
            CheckSessionAuthority(chatSession, user);
            chatSession.LastId++;
            var message = messagePayload.ToMessage(chatSession.LastId, user.Id);
            chatSession.Messages = new List<Message>() { message };
            await DbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMessage), new {id=id, messageId=message.Id}, MessageResponse.From(message));
        }

        /**
         * <summary>获取某条信息。</summary>
         */
        [HttpGet("{id:long}/Messages/{messageId:long}")]
        public async Task<MessageResponse> GetMessage(long id, long messageId)
        {
            var (user, chatSession) = await GetUserAndChatSession(id);
            CheckSessionAuthority(chatSession, user);
            var messageResult = await DbContext.Messages
                .Where(m => m.ChatSessionId == chatSession.Id && m.IdPerChat == messageId).ToListAsync();
            if (messageResult.Count != 1) throw new Http404NotFoundException("Message is not found.");
            return MessageResponse.From(messageResult[0]);
        }

        private async Task<(User, ChatSession)> GetUserAndChatSession(long sessionId)
        {
            var user = await UserService.GetUser(this);
            var chatSession = await DbContext.ChatSessions.Include(cs => cs.Participants).SingleAsync(cs => cs.Id == sessionId);
            return (user, chatSession);
        }

        private static void CheckSessionAuthority(ChatSession session, User user)
        {
            if (!session.Participants.Contains(user))
                throw new Http403ForbiddenException(
                    "Current user can't get this session, because it doesn't participant in it.");
        }

        private static void CheckMessagePayload(MessageBase message)
        {
            if (message.SentTime > DateTime.Now)
                throw new Http400BadRequestException("The message can't be sent later than now.");
            message.SentTime = DateTime.SpecifyKind(message.SentTime, DateTimeKind.Utc);
        }
    }
}
