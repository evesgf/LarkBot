using Newbe.Mahua.MahuaEvents;
using System;
using Lark.Bot.CQA.Handler;
using Newbe.Mahua;
using Lark.Bot.CQA.Handler.GroupMessageHandler;
using Lark.Bot.CQA.Handler.TimeJobHandler;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GroupMessageReceivedMahuaEvent1
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IGroupMessageHandler _groupMessageHandler;
        private readonly ITimeJobHandler _timeJobHandler;

        public GroupMessageReceivedMahuaEvent1(
            IMahuaApi mahuaApi, IGroupMessageHandler groupMessageHandler, ITimeJobHandler timeJobHandler)
        {
            _mahuaApi = mahuaApi;
            _groupMessageHandler = groupMessageHandler;
            _timeJobHandler = timeJobHandler;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var result = _groupMessageHandler.CheckKeyWord(context.Message);

            if (result.IsHit)
            {
                _mahuaApi.SendGroupMessage(context.FromGroup, result.Msg);
            }
        }
    }
}
