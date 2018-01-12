
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
                var reMsg = RequestHandler.OffSitePrice();
                //回发
                _mahuaApi.SendGroupMessage(context.FromGroup, reMsg);
            }

            // 不要忘记在MahuaModule中注册
        }
    }
}
