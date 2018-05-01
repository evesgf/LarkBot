using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Uitls
{
    /// <summary>
    /// https://www.cnblogs.com/DamonCoding/p/8475466.html
    /// </summary>
    public static class HttpUitls
    {
        public static async Task<HttpResult> HttpGetRequestAsync(string url)
        {
            return await Task.Run(()=> {
                HttpResult httpResult = new HttpResult();

                var getRequest = HttpWebRequest.Create(url) as HttpWebRequest;
                getRequest.Method = "GET";
                getRequest.Timeout = 10000;
                getRequest.ContentType = "text/html;charset=UTF-8";
                getRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                try
                {
                    var myResponse = (HttpWebResponse)getRequest.GetResponse();

                    using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        httpResult.StrResponse = reader.ReadToEnd();
                        httpResult.Success = true;
                    }

                }
                //异常请求  
                catch (WebException e)
                {
                    httpResult.StrResponse = e.Message;
                    httpResult.Success = false;
                }

                return httpResult;
            });
        }

        public static string Get(string url)
        {
            string responseResult = string.Empty;
            var getRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            getRequest.Method = "GET";
            getRequest.Timeout = 10000;
            getRequest.ContentType = "text/html;charset=UTF-8";
            getRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                var myResponse = (HttpWebResponse)getRequest.GetResponse();

                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    responseResult = reader.ReadToEnd();
                }

            }
            //异常请求  
            catch (WebException e)
            {
                responseResult = e.Message;
            }

            return responseResult;
        }

        public static string Post(string Url, string Data, string Referer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Referer = Referer;
            byte[] bytes = Encoding.UTF8.GetBytes(Data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return retString;
        }

        public static string HttpsGet(string Url)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "Get";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            //异常请求  
            catch (WebException e)
            {
                return e.ToString();
                //myResponse = (HttpWebResponse)e.Response;
                //using (Stream errData = myResponse.GetResponseStream())
                //{
                //    using (StreamReader reader = new StreamReader(errData))
                //    {
                //        string text = reader.ReadToEnd();

                //        return text;
                //    }
                //}
            }
        }

        public static string HttpsPost(string Url, string Data, string Referer)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Referer = Referer;
            byte[] bytes = Encoding.UTF8.GetBytes(Data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);
            request.ProtocolVersion = HttpVersion.Version10;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return retString;
        }

        public static async Task<HttpResult> HttpsGetRequestAsync(string url)
        {
            return await Task.Run(() =>
            {
                HttpResult httpResult = new HttpResult();

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "Get";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                try
                {
                    var myResponse = (HttpWebResponse)myRequest.GetResponse();

                    using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        httpResult.StrResponse = reader.ReadToEnd();
                        httpResult.Success = true;
                    }

                }
                //异常请求  
                catch (WebException e)
                {
                    httpResult.StrResponse = e.Message;
                    httpResult.Success = false;
                }

                return httpResult;

                return httpResult;
            });
        }
    }

    /// <summary>
    /// Http请求的返回结果
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 详细报文
        /// </summary>
        public string StrResponse { get; set; }
    }
}
