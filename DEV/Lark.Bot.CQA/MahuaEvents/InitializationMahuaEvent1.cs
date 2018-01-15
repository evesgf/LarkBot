using Lark.Bot.CQA.Handler.TimeJobHandler;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;

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
        private readonly ITrackHandler _trackHandler;

        public InitializationMahuaEvent1(
            IMahuaApi mahuaApi,ITimeJobHandler timeJobHandler,ITrackHandler trackHandler)
        {
            _mahuaApi = mahuaApi;
            _timeJobHandler = timeJobHandler;
            _trackHandler = trackHandler;
        }

        public void Initialized(InitializedContext context)
        {
            //币圈消息推送
            _timeJobHandler.StartPushNews("693739965");

            _trackHandler.StartLoop();
        }
    }
}
