using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class HttpUitls
{
    public static string Get(string Url,string contentType= "application/json; charset=UTF-8")
    {

        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

        //System.GC.Collect();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Proxy = null;
        request.KeepAlive = false;
        request.Method = "GET";
        request.ContentType = contentType;
        request.AutomaticDecompression = DecompressionMethods.GZip;

        request.ProtocolVersion = HttpVersion.Version10;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
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

    private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true; //总是接受  
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
}
