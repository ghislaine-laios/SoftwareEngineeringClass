#pragma warning disable CS1591
namespace WebApi.Model
{
    /**
     * 聊天会话的实体定义
     */
    public class ChatSession
    {
        public long Id { get; set; }

        public long LastId { get; set; }

        // Relations
        public IList<User> Participants { get; set; }
        public IList<Message> Messages { get; set; }
    }
}
