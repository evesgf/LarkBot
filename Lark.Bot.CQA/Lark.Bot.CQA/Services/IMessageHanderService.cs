using Newbe.Mahua.MahuaEvents;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface IMessageHanderService
    {
        /// <summary>
        /// 检查关键词
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string CheckKeyWordAsync(GroupMessageReceivedContext context);
    }
}
