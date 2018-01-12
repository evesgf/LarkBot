using System;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Crawler
{
    public interface ICrawler
    {
        /// <summary>
        /// 爬虫启动事件
        /// </summary>
        event EventHandler<OnStartEventArgs> OnStart;            
        /// <summary>
        /// 爬虫完成事件
        /// </summary>
        event EventHandler<OnCompletedEventArgs> OnCompleted;    
        /// <summary>
        /// 爬虫出错事件
        /// </summary>
        event EventHandler<Exception> OnError;                   

        /// <summary>
        /// 定义Cookie容器
        /// </summary>
        CookieContainer CookieContainer { get; set; }            

        /// <summary>
        /// 异步创建爬虫
        /// </summary>
        /// <param name="uri">爬虫URL地址</param>
        /// <param name="proxy">代理服务器</param>
        /// <returns>网页源代码</returns>
        Task<string> Start(Uri uri, IWebProxy proxy = null);
    }
}
