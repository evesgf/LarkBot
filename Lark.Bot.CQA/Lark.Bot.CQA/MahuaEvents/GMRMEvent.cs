using Lark.Bot.CQA.Services;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GMRMEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        //private readonly IOkexService _okexService;
        private readonly IMessageHanderService _iMessageHanderService;

        public GMRMEvent(
            IMahuaApi mahuaApi,
            IMessageHanderService iMessageHanderService)
        {
            _mahuaApi = mahuaApi;

            _iMessageHanderService = iMessageHanderService;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            //[看币价 btc]
            var re_coinmarketcap =_iMessageHanderService.CheckKeyWordAsync(context);
            if (!string.IsNullOrEmpty(re_coinmarketcap))
                _mahuaApi.SendGroupMessage(context.FromGroup, re_coinmarketcap);

            // 不要忘记在MahuaModule中注册
        }
    }
}
