using Newbe.Mahua.MahuaEvents;
using System.Threading.Tasks;
using static Lark.Bot.CQA.Services.CoinmarketcapService;

namespace Lark.Bot.CQA.Services
{
    public interface ICoinmarketcapService
    {
        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns></returns>
        CoinmarketcapTicker[] GetTickerList();

        /// <summary>
        /// 通过Key查询币价
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetTicker(string key);
    }
}
