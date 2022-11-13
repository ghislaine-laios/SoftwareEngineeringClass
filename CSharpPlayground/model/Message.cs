using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayground.model
{
    /**
     * 聊天信息的实体
     */
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }

        //Relations
        public User Sender { get; set; }
    }
}
