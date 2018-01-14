using Lark.Bot.CQA.Modules;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent1
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IMsgHandler _msgHandler;

        public PrivateMessageFromFriendReceivedMahuaEvent1(
            IMahuaApi mahuaApi, IMsgHandler msgHandler)
        {
            _mahuaApi = mahuaApi;
            _msgHandler = msgHandler;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            // todo 填充处理逻辑
            // 不要忘记在MahuaModule中注册

            //币圈消息处理
            var reCoin = _msgHandler.CheckKeyWords(context.Message);
            if (reCoin.HasKeyWord)
            {
                _mahuaApi.SendPrivateMessage(context.FromQq , reCoin.ResultMsg);
                return;
            }
        }
    }
}
