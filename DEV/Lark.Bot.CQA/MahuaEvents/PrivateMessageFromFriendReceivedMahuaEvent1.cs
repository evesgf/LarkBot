using Newbe.Mahua.MahuaEvents;
using System;
using Newbe.Mahua;
using Lark.Bot.CQA.Handler.PrivateMessageHandler;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent1
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IPrivateMessageHandler _privateMessageHandler;

        public PrivateMessageFromFriendReceivedMahuaEvent1(
            IMahuaApi mahuaApi, IPrivateMessageHandler privateMessageHandler)
        {
            _mahuaApi = mahuaApi;
            _privateMessageHandler = privateMessageHandler;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            // todo 填充处理逻辑
            var result = _privateMessageHandler.CheckKeyWord(context);

            if (result.IsHit)
            {
                _mahuaApi.SendPrivateMessage(context.FromQq, result.Msg);
            }
        }
    }
}
