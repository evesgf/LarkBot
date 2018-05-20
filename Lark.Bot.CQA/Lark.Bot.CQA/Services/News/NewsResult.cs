using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.News
{
    /// <summary>
    /// 新闻对象
    /// </summary>
    public class NewsResult
    {
        //新闻爬取是否成功，不成功则把消息放到Content里
        public bool Success { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NewsLevel NewsLevel { get; set; } = NewsLevel.Normal;
    }

    /// <summary>
    /// 新闻重要等级
    /// </summary>
    public enum NewsLevel
    {
        Normal,
        Importent
    }
}
