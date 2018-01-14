using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Crawler
{
    /// <summary>
    /// 简单爬虫
    /// https://www.cnblogs.com/jjg0519/p/6702747.html
    /// </summary>
    public class LittleCrawler: ICrawler
    {
        public event EventHandler<OnStartEventArgs> OnStart;            //爬虫启动事件
        public event EventHandler<OnCompletedEventArgs> OnCompleted;    //爬虫完成事件
        public event EventHandler<Exception> OnError;                   //爬虫出错事件

        public CookieContainer CookieContainer { get; set; }            //定义Cookie容器

        public LittleCrawler() { }

        /// <summary>
        /// 异步创建爬虫
        /// TODO:多线程下调用BUG,原因未查明
        /// </summary>
        /// <param name="uri">爬虫URL地址</param>
        /// <param name="proxy">代理服务器</param>
        /// <returns>网页源代码</returns>
        public async Task<string> StartAsync(Uri uri, IWebProxy proxy = null)
        {
            #region 异步爬虫 有多线程BUG
            //TODO:多线程下调用BUG
            return await Task.Run(() => {
                var pageSource = string.Empty;

                try
                {
                    //if (this.OnStart != null) OnStart(this, new OnStartEventArgs(uri));
                    OnStart?.Invoke(this, new OnStartEventArgs(uri));

                    var watch = new Stopwatch();
                    watch.Start();

                    var request = WebRequest.Create(uri) as HttpWebRequest;
                    request.Accept = "*/*";
                    request.ContentType = "application/json; charset=UTF-8";      //定义文档类型和编码
                    //request.allowAutoRedirect = false;                              //禁止自动跳转
                    //request.UserAgent = "";                                         //伪装谷歌浏览器
                    request.ContinueTimeout = 5000;//请求超时时间
                    request.Method = "GET";//定义请求方式
                    if (proxy != null) request.Proxy = proxy;
                    request.CookieContainer = this.CookieContainer;

                    var task = request.GetResponseAsync();
                    WebResponse wResp = task.Result;
                    System.IO.Stream respStream = wResp.GetResponseStream();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream))
                    {
                        pageSource = reader.ReadToEnd();
                    }

                    watch.Stop();
                    var threadId = Thread.CurrentThread.ManagedThreadId;            //获取当前任务线程ID
                    var milliseconds = watch.ElapsedMilliseconds;                   //获取请求执行时间

                    request.Abort();

                    OnCompleted?.Invoke(this, new OnCompletedEventArgs(uri, threadId, pageSource, milliseconds));
                }
                catch (Exception ex)
                {
                    if (this.OnError != null) OnError(this, ex);
                }

                return pageSource;
            });
            #endregion
        }

        /// <summary>
        /// 同步创建爬虫
        /// </summary>
        /// <param name="uri">爬虫URL地址</param>
        /// <param name="proxy">代理服务器</param>
        /// <returns>网页源代码</returns>
        public string Start(Uri uri, IWebProxy proxy = null)
        {
            var pageSource = string.Empty;

            try
            {
                //if (this.OnStart != null) OnStart(this, new OnStartEventArgs(uri));
                OnStart?.Invoke(this, new OnStartEventArgs(uri));

                var watch = new Stopwatch();
                watch.Start();

                var request = WebRequest.Create(uri) as HttpWebRequest;
                request.Accept = "*/*";
                request.ContentType = "application/json; charset=UTF-8";      //定义文档类型和编码
                                                                              //request.allowAutoRedirect = false;                              //禁止自动跳转
                                                                              //request.UserAgent = "";                                         //伪装谷歌浏览器
                request.ContinueTimeout = 5000;//请求超时时间
                request.Method = "GET";//定义请求方式
                if (proxy != null) request.Proxy = proxy;
                request.CookieContainer = this.CookieContainer;

                var task = request.GetResponseAsync();
                WebResponse wResp = task.Result;
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream))
                {
                    pageSource = reader.ReadToEnd();
                }

                watch.Stop();
                var threadId = Thread.CurrentThread.ManagedThreadId;            //获取当前任务线程ID
                var milliseconds = watch.ElapsedMilliseconds;                   //获取请求执行时间

                request.Abort();

                OnCompleted?.Invoke(this, new OnCompletedEventArgs(uri, threadId, pageSource, milliseconds));
            }
            catch (Exception ex)
            {
                if (this.OnError != null) OnError(this, ex);
            }

            return pageSource;
        }
    }

    /// <summary>
    /// 爬虫启动事件
    /// </summary>
    public class OnStartEventArgs
    {
        public Uri Uri { get; set; }                                    //爬虫URL地址

        public OnStartEventArgs(Uri uri)
        {
            this.Uri = uri;
        }
    }

    /// <summary>
    /// 爬虫完成事件
    /// </summary>
    public class OnCompletedEventArgs
    {
        public Uri Uri { get; private set; }                            //爬虫URL地址
        public int ThreadId { get; private set; }                       //任务线程ID
        public string PageSource { get; private set; }                   //页面源代码
        public long Milliseconds { get; private set; }                  //爬虫请求执行时间

        public OnCompletedEventArgs(Uri uri, int threadId, string pageSource, long milliseconds)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
        }
    }
}
