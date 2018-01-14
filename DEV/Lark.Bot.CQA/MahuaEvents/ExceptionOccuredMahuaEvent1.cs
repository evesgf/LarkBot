using Newbe.Mahua.MahuaEvents;
using System;
using Newbe.Mahua;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 运行出现异常事件
    /// </summary>
    public class ExceptionOccuredMahuaEvent1
        : IExceptionOccuredMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public ExceptionOccuredMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void HandleException(ExceptionOccuredContext context)
        {
            _mahuaApi.SendPrivateMessage("821113542","锅咩~程序跪了:"+ context.Exception);

            // 不要忘记在MahuaModule中注册
        }
    }
}
