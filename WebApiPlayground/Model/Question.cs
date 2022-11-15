#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPlayground.Model
{
    public enum QuestionStatus
    {
        Waiting, Solving, Solved
    }

    public class QuestionBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionStatus Status { get; set; }
    }

    /**
    * 问题实体的定义
    */
    public class Question: QuestionBase
    {
        // Relations
        public User Sender { get; set; } // 提问者
        public User? Solver { get; set; } // 解决者
        public ChatSession? Session { get; set; } // 问题的聊天会话
    }

    public class QuestionFactory {}
}
