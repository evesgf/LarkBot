using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Models
{
    class NewsList
    {
        public int Id { get; set; }
        /// <summary>
        /// 来源站名称
        /// </summary>
        public string NewsFrom { get; set; }
        /// <summary>
        /// 爬取页面的URL
        /// </summary>
        public string NewsFromUrl { get; set; }
        /// <summary>
        /// 文章发表时间(时间戳)
        /// </summary>
        public long NewsPublishTime { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string NewsTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 创建时间(时间戳)
        /// </summary>
        public long NewsCreateTime { get; set; }
    }
}
