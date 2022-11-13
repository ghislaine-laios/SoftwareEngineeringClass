using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayground.model
{
    /**
     * 用户实体的定义
     */
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        
        // Relations
        public IList<ChatSession> ChatSessions { get; set; }
    }
}
