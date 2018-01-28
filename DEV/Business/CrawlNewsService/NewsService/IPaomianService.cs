using Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.NewsService
{
    public interface IPaomianService: IDependencyRegister
    {
        /// <summary>
        /// 更新泡面小镇的早报
        /// </summary>
        /// <returns></returns>
        Task<CrawlerResult<CrawlNews>> UpdateNews();

        /// <summary>
        /// 获取最新一条泡面小镇的早报
        /// </summary>
        /// <returns></returns>
        Task<CrawlNews> GetLatestNews();
    }
}
