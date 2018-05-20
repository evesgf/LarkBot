using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Uitls.Config
{
    public class PushNewsConfig: ConfigBase
    {
        /// <summary>
        /// 新闻站地址
        /// </summary>
        public string NewsServerURL { get; set; }
        /// <summary>
        /// 轮询检查时间(秒)
        /// </summary>
        public int LoopCheckTime { get; set; }
        /// <summary>
        /// 是否比较相似性
        /// </summary>
        public bool UsCompute { get; set; }
        /// <summary>
        /// 相似性阙值
        /// </summary>
        public float ComputeRate { get; set; }
        /// <summary>
        /// 币圈推送群组
        /// </summary>
        public string[] CoinNewsPushGroupList { get; set; }
        /// <summary>
        /// normal类新闻间隔推送次数
        /// </summary>
        public int NormalNewsPushIntervalCount { get; set; }

        /// <summary>
        /// 早报推送群组
        /// </summary>
        public string[] MorningPaperPushGroupList { get; set; }
        /// <summary>
        /// 早报推送群组
        /// </summary>
        public string[] MorningPaperPushPrivateList { get; set; }
    }
}
