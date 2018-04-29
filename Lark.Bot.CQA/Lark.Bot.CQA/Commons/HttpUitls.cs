using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Commons
{
    public static class HttpUitls
    {
        public static string Get(string Url, string Referer = null, string cross = null)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "GET";

            if (Referer != null)
            {
                myRequest.Referer = Referer;
            }

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (cross != null)
                {
                    myResponse.Headers.Add("Access-Control-Allow-Origin", cross);
                }

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
    }
}
