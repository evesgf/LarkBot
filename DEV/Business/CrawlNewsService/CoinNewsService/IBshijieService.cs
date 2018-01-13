using Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.CoinNewsService
{
    public interface IBshijieService:IDependencyRegister
    {
        /// <summary>
        /// 更新币世界推送的快讯
        /// </summary>
        /// <returns></returns>
        Task<CrawlerResult<CrawlNews>> UpdatePushNewsFlash();

        /// <summary>
        /// 获取最新一条快讯
        /// </summary>
        /// <returns></returns>
        Task<CrawlNews> GetLatestNewsFlash();
    }
}
