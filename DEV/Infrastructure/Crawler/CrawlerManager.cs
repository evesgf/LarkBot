using Infrastructure.Common;
using Infrastructure.Crawler;

namespace Core.Crawler
{
    public class CrawlerManager : Singleton<CrawlerManager>
    {
        /// <summary>
        /// 获取一个爬虫
        /// TODO:爬虫池
        /// </summary>
        /// <returns></returns>
        public ICrawler GetCrawler()
        {
            return new LittleCrawler();
        }
    }
}
