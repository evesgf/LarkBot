using Data.Entity;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.CoinNewsService
{
    public interface IJinseService:IDependencyRegister
    {
        /// <summary>
        /// 更新金色财经推送的快讯
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
