#pragma warning disable CS1591
namespace WebApi.Model
{
    public class UserBase
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
    }

    /**
     * 用户实体的定义
     */
    public class User: UserBase
    {
        // Relations
        public IList<ChatSession> ChatSessions { get; set; }
    }
}
