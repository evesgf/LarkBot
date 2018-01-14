namespace Lark.Bot.CQA.Handler
{
    public interface IHandler
    {
        /// <summary>
        /// 传入关键词判断
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        HandlerResult CheckKeyWord(string context);
    }
}