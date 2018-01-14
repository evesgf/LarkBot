using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler
{
    public class HandlerResult
    {
        /// <summary>
        /// 是否命中
        /// </summary>
        public bool IsHit { get; set; }
        /// <summary>
        /// 命中后返回的消息
        /// </summary>
        public string Msg { get; set; }
    }
}
