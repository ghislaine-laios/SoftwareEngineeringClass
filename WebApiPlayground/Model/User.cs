#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPlayground.Model
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
