using Business.CrawlNewsService.CoinNewsService;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Core.TimeJob
{
    public class TimeJobManager:Singleton<TimeJobManager>
    {
        Timer t = new Timer(1000 * 60 * 3);

        public void OpenTimeJob()
        {
            try
            {
                // todo 填充处理逻辑
                t.Elapsed += new System.Timers.ElapsedEventHandler(UpdateNews);
                t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNews(object source, System.Timers.ElapsedEventArgs e)
        {
            var a=HttpGet("http://larkbot.evesgf.com/api/News/UpdateJinseNews", null);
            var b= HttpGet("http://larkbot.evesgf.com/api/News/UpdateBishijieNews", null);
            var c= HttpGet("http://larkbot.evesgf.com/api/News/UpdateBitcoinNews", null);
            var d= HttpGet("http://larkbot.evesgf.com/api/News/UpdateOkexNoticeFlash", null);
        }

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
