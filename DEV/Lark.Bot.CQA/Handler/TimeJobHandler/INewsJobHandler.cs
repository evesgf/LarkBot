using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    public interface INewsJobHandler
    {
        /// <summary>
        /// 开启推送早报到群组
        /// </summary>
        /// <param name="groupQQ"></param>
        bool StartPushNews(string[] groupQQList);
    }
}
