using Newbe.Mahua.MahuaEvents;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface ICoinmarketcapService
    {

        /// <summary>
        /// 通过Key查询币价
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetTicker(string key);
    }
}
