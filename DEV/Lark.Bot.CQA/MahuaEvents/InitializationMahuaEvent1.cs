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
        private readonly INewsJobHandler _newsJobHandler;

        public InitializationMahuaEvent1(
            IMahuaApi mahuaApi,ITimeJobHandler timeJobHandler,ITrackHandler trackHandler, INewsJobHandler newsJobHandler)
        {
            _mahuaApi = mahuaApi;
            _timeJobHandler = timeJobHandler;
            _trackHandler = trackHandler;
            _newsJobHandler = newsJobHandler;
        }

        public void Initialized(InitializedContext context)
        {
            //测试群 376624308

            //币圈消息推送
            _timeJobHandler.StartPushNews("693739965");

            //早报推送
            _newsJobHandler.StartPushNews(new string[] { "376624308" });

            _trackHandler.StartLoop();
        }
    }
}
