using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Models
{
    /// <summary>
    /// OKEX币价
    /// </summary>
    public class Market
    {
        /// <summary>
        /// 返回数据时服务器时间
        /// </summary>
        public string date { get; set; }
        public Tacker ticker { get; set; }
    }

    public class Tacker
    {
        /// <summary>
        /// 买一价
        /// </summary>
        public decimal buy { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal high { get; set; }
        /// <summary>
        ///  最新成交价
        /// </summary>
        public decimal last { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal low { get; set; }
        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal sell { get; set; }
        /// <summary>
        /// 成交量(最近的24小时)
        /// </summary>
        public decimal vol { get; set; }
    }
}
