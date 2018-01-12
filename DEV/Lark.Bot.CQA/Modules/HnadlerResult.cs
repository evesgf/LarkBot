using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules
{
    public class HnadlerResult
    {
        /// <summary>
        /// 是否命中关键词
        /// </summary>
        public bool HasKeyWord { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string ResultMsg { get; set; }
    }
}
