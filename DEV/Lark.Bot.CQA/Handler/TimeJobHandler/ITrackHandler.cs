using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    public interface ITrackHandler
    {
        /// <summary>
        /// 开启监控循环
        /// </summary>
        void StartLoop();

        /// <summary>
        /// 开启币价监听追踪
        /// model中fromQQ和fromGroup都填的时候是群成员私聊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool StartTrackCoinPrice(TrackPriceModel model);

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="fromQQ"></param>
        /// <returns></returns>
        bool StopTrackCoinPrice(TrackPriceModel model);
    }
}
