using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class NewsModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 重要性等级
        /// </summary>
        public int ImportantLevel { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>
        public string FromUrl { get; set; }
        /// <summary>
        /// 来源方推送时间
        /// </summary>
        public string PushTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
    }
}
