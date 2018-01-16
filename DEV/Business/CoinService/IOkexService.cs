using Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoinService
{
    public interface IOkexService: IDependencyRegister
    {

        /// <summary>
        /// 更新OKEX的公告
        /// </summary>
        /// <returns></returns>
        Task<CrawlerResult<CrawlNews>> UpdateNoticeFlash();

        /// <summary>
        /// 获取最新一条公告
        /// </summary>
        /// <returns></returns>
        Task<CrawlNews> GetLatestNotice();
    }
}
