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
        /// 法币买一
        /// </summary>
        /// <returns></returns>
        Task<CrawlerResult<string>> LegalTenderBuy();

        /// <summary>
        /// 法币卖一
        /// </summary>
        /// <returns></returns>
        Task<CrawlerResult<string>> LegalTenderSell();
    }
}
