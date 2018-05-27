using AngleSharp.Parser.Html;
using Lark.Bot.CQA.Uitls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.News
{
    public class BishijieService : IBishijieService
    {
        public async Task<NewsResult> GetLatestNewsFlash()
        {
            var reNews = new NewsResult();

            var url = "http://www.bishijie.com/kuaixun/";
            HttpResult httpResult = await HttpUitls.HttpsGetRequestAsync(url);

            if (httpResult.Success)
            {
                //HTML 解析成 IDocument
                var htmlParser = new HtmlParser();
                var dom = htmlParser.Parse(httpResult.StrResponse);
                //解析页面
                var listRoot = dom.QuerySelector(".kuaixun_list");
                var firstNew = listRoot.QuerySelector("ul");
                var title = firstNew.QuerySelector("a").GetAttribute("title");
                var content = firstNew.QuerySelector(".lh32");

                reNews.Success = true;
                reNews.From = "【币世界】";
                reNews.Title = title;
                reNews.Content =content.TextContent.Replace(title, "").Replace(" ", "").Trim();

                //重要性判断
                if (firstNew.QuerySelector("a").GetAttribute("style")!=null && firstNew.QuerySelector("a").GetAttribute("style") == "color:#ff0000;")
                {
                    reNews.NewsLevel = NewsLevel.Importent;
                }
                else
                {
                    reNews.NewsLevel = NewsLevel.Normal;
                }
            }
            else
            {
                reNews.Success = false;
                reNews.Content = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                reNews.Content += "oh~锅咩锅咩~程序跪了~";
            }

            return reNews;
        }
    }
}
