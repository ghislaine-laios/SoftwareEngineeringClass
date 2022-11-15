﻿#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPlayground.Model
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
