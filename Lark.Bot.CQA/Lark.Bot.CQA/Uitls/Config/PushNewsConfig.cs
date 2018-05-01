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
        /// 推送群组
        /// </summary>
        public string[] PushGroupList { get; set; }
    }
}
