#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPlayground.Model
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
