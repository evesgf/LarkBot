using Newbe.Mahua.MahuaEvents;
using System;
using Lark.Bot.CQA.Handler;
using Newbe.Mahua;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GroupMessageReceivedMahuaEvent1
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IHandler _handler;

        public GroupMessageReceivedMahuaEvent1(
            IMahuaApi mahuaApi, IHandler handler)
        {
            _mahuaApi = mahuaApi;
            _handler = handler;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var result = _handler.CheckKeyWord(context.Message);

            if (result.IsHit)
            {
                _mahuaApi.SendGroupMessage(context.FromGroup, result.Msg);
            }
        }
    }
}
