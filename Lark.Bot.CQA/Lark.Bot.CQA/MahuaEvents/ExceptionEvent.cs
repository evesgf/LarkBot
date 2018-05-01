using Lark.Bot.CQA.Uitls.Config;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 运行出现异常事件
    /// </summary>
    public class ExceptionEvent
        : IExceptionOccuredMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public ExceptionEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void HandleException(ExceptionOccuredContext context)
        {
            // todo 填充处理逻辑
            if (ConfigManager.larkBotConfig == null)
            {
                _mahuaApi.SendPrivateMessage("821113542", context.Exception.ToString());
            }
            else
            {
                _mahuaApi.SendPrivateMessage(ConfigManager.larkBotConfig.AdminQQ, context.Exception.ToString());
            }

            // 不要忘记在MahuaModule中注册
        }
    }
}
