using AngleSharp.Parser.Html;
using Lark.Bot.CQA.Uitls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.News
{
    public class PmtownService:IPmtownService
    {
        public async Task<string> GetMorningPapaer()
        {
            string reNews = string.Empty;

            var url = "http://www.pmtown.com/archives/category/%E6%97%A9%E6%8A%A5";
            HttpResult httpResult = await HttpUitls.HttpsGetRequestAsync(url);

            if (httpResult.Success)
            {
                //HTML 解析成 IDocument
                var htmlParser = new HtmlParser();
                var dom = htmlParser.Parse(httpResult.StrResponse);
                //解析页面
                var listRoot = dom.QuerySelector(".article-list");
                var firstNewURL = listRoot.QuerySelector(".item-title").QuerySelector("a").GetAttribute("href");
                if (!string.IsNullOrEmpty(firstNewURL))
                {
                    HttpResult httpDetialResult = await HttpUitls.HttpsGetRequestAsync(firstNewURL);
                    var domDetial = htmlParser.Parse(httpDetialResult.StrResponse);
                    var title = domDetial.QuerySelector(".entry-title").TextContent;
                    var content = domDetial.QuerySelector(".entry-content");
                    //掐头去尾
                    var contentTxt = content.TextContent.Trim();

                    reNews =title + "\n"+ contentTxt.Substring(0, contentTxt.Length- 97);
                }
                else
                {
                    reNews = null;
                }
            }
            else
            {
                reNews = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                reNews = "oh~锅咩锅咩~程序跪了~";
            }

            return reNews;
        }
    }
}
