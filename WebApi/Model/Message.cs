#pragma warning disable CS1591
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class MessageBase
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
    }

    public class MessageEntity : MessageBase
    {
        public long IdPerChat { get; set; }
        
        // Relations
        public long SenderId { get; set; }

        public long ChatSessionId { get; set; }
    }

    /**
     * 聊天信息的实体
     */
    public class Message: MessageEntity
    {
        //Relations
        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

    }
}
