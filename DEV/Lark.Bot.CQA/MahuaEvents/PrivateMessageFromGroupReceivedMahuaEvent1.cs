using Lark.Bot.CQA.Handler.GroupPrivateMsgHander;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 来自群成员的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromGroupReceivedMahuaEvent1
        : IPrivateMessageFromGroupReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        //private readonly IGroupPrivateMsgHander _groupPrivateMsgHander;

        public PrivateMessageFromGroupReceivedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
            //_groupPrivateMsgHander = groupPrivateMsgHander;
        }

        public void ProcessGroupMessage(PrivateMessageFromGroupReceivedContext context)
        {
            //// todo 填充处理逻辑
            //var result = _groupPrivateMsgHander.CheckKeyWord(context);

            //if (result.IsHit)
            //{
            //    _mahuaApi.SendGroupMessage(context.FromGroup, result.Msg);
            //}
        }
    }
}
