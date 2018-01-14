
using Lark.Bot.CQA.Modules.TrackCoin;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GroupMessageReceivedMahuaEvent1
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMessageReceivedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            if (context.Message.Equals("早报"))
            {
                //查询早报
                var reMsg = RequestHandler.RequestZaoBaoApi();
                //回发
                _mahuaApi.SendGroupMessage(context.FromGroup, reMsg);
            }

            if (context.Message.Equals("历史早报"))
            {
                //查询早报
                var reMsg = RequestHandler.RequestLastZaoBaoApi();
                //回发
                _mahuaApi.SendGroupMessage(context.FromGroup, reMsg);
            }

            if (context.Message.Equals("币圈消息"))
            {
                //查询币圈
                var reMsg = RequestHandler.RequestBiQuanApi() + "\n" + RequestHandler.GetBitPrice("btc_usdt");
                //回发
                _mahuaApi.SendGroupMessage(context.FromGroup, reMsg);
            }

            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Message.Remove(0, 4);

                //使用CoolQApi将信息回发给发送者
                _mahuaApi.SendGroupMessage(context.FromGroup, RequestHandler.GetBitPrice(key));
            }

            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("看币价 "))
            {
                string key = context.Message.Remove(0, 4);

                //使用CoolQApi将信息回发给发送者
                _mahuaApi.SendGroupMessage(context.FromGroup, RequestHandler.GetBitPrice2(key));
            }

            // todo 填充处理逻辑
            if (context.Message.Equals("场外币价"))
            {
                //查询场外币价
                var reMsg = RequestHandler.OTCPrice();
                //回发
                _mahuaApi.SendGroupMessage(context.FromGroup, reMsg);
            }

            //币价监控
            //格式为：币价监控 okex nas_btc 小于 10000
            if (context.Message.Substring(0, 4).Equals("币价监控"))
            {
                var keys = context.Message.Split(' ');
                if (keys.Length != 5) _mahuaApi.SendGroupMessage(context.FromGroup, "指令错误");

                var up = false;
                up = (keys[3].Equals("大于")) ? true : false;

                Singleton<TrackManager>.Instance.AddTrack(keys[2],new TrackInfoModel { coin=keys[2],isUp= up,price= Convert.ToDecimal(keys[4]),fromQQ=context.FromQq,fromGroup=context.FromGroup});
            }

            //移除监控
            //格式为：移除监控 okex nas_btc
            if (context.Message.Substring(0, 4).Equals("移除监控"))
            {
                var keys = context.Message.Split(' ');
                if (keys.Length != 2) _mahuaApi.SendGroupMessage(context.FromGroup, "指令错误");

                Singleton<TrackManager>.Instance.RemoveTrack(keys[1],new TrackInfoModel { fromGroup=context.FromGroup});
            }

            // 不要忘记在MahuaModule中注册
        }
    }
}
