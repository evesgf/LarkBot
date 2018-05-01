using Autofac;
using Autofac.Extras.Quartz;
using Lark.Bot.CQA.Uitls.Config;
using Lark.Bot.CQA.TimeJobs;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Quartz;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 插件初始化事件
    /// </summary>
    public class InitEvent
        : IInitializationMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        private readonly ICoinNewsTimeJob _coinNewsTimeJob;

        public InitEvent(
            IMahuaApi mahuaApi, ICoinNewsTimeJob coinNewsTimeJob)
        {
            _mahuaApi = mahuaApi;

            _coinNewsTimeJob = coinNewsTimeJob;
        }

        public void Initialized(InitializedContext context)
        {
            // todo 填充处理逻辑

            //配置初始化
            ConfigManager.Create();

            //开始推送新闻
            _coinNewsTimeJob.StartPushNews();

            _mahuaApi.SendPrivateMessage(ConfigManager.larkBotConfig.AdminQQ, "LarkBot初始化完成");
            // 不要忘记在MahuaModule中注册
        }
    }
}
