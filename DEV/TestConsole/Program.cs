using System;
using System.IO;
using System.Net;
using System.Text;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var restr = HttpGet("https://api.huobi.pro/market/detail/merged?symbol=btcusdt");
            var restr = HttpGet("https://www.okex.com/api/v1/ticker.do?symbol=btc_usdt");
            //var restr2 = HttpUitls.Get("https://www.okex.com/api/v1/ticker.do?symbol=btc_usdt");

            Console.WriteLine(restr);
            //Console.WriteLine(restr2);

            Console.ReadKey(true);
        }

        /// <summary>  
        /// GET Method  
        /// </summary>  
        /// <returns></returns>  
        public static string HttpGet(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(),Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            //异常请求  
            catch (WebException e)
            {
                myResponse = (HttpWebResponse)e.Response;
                using (Stream errData = myResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        string text = reader.ReadToEnd();

                        return text;
                    }
                }
            }
        }
    }

    
}
