using Lark.Bot.CQA.Commons.Config;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
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

        public InitEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Initialized(InitializedContext context)
        {
            // todo 填充处理逻辑
            ConfigManager.Create();

            _mahuaApi.SendPrivateMessage(ConfigManager.larkBotConfig.AdminQQ, "LarkBot初始化完成");
            // 不要忘记在MahuaModule中注册
        }
    }
}
