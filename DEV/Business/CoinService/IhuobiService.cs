using Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Coin
{
    /// <summary>
    /// 火币网抓取服务
    /// </summary>
    public interface IhuobiService:IDependencyRegister
    {
        /// <summary>
        /// 法币价格查询
        /// </summary>
        /// <param name="coinId">coin值1为btc,2为usdt</param>
        /// <param name="tradeType">tradeType值1为买0为卖</param>
        /// <returns></returns>
        Task<CrawlerResult<string>> LegalTender(int coinId, int tradeType);
    }
}
