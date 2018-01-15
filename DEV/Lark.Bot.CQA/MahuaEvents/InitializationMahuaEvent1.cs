using Newbe.Mahua.MahuaEvents;
using System;
using Newbe.Mahua;
using Lark.Bot.CQA.Business;
using Lark.Bot.CQA.Handler.TimeJobHandler;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 插件初始化事件
    /// </summary>
    public class InitializationMahuaEvent1
        : IInitializationMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly ITimeJobHandler _timeJobHandler;

        public InitializationMahuaEvent1(
            IMahuaApi mahuaApi,ITimeJobHandler timeJobHandler)
        {
            _mahuaApi = mahuaApi;
            _timeJobHandler = timeJobHandler;
        }

        public void Initialized(InitializedContext context)
        {
            _timeJobHandler.StartPushNews("693739965");
        }
    }
}
