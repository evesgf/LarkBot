using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    /// <summary>
    /// TODO:不能继承IHander,待处理BUG
    /// </summary>
    public interface ITimeJobHandler
    {
        /// <summary>
        /// 开启推送新闻到群组
        /// </summary>
        /// <param name="groupQQ"></param>
        bool StartPushNews(string groupQQ);
    }
}
