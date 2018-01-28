using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Business.CrawlNewsService.CoinNewsService;
using Core.Crawler;
using Data.Entity;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.NewsService
{
    public class PaomianService : IPaomianService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaomianService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 更新泡面小镇的早报
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlerResult<CrawlNews>> UpdateNews()
        {
            var result = new CrawlerResult<CrawlNews>();

            var crawler = Singleton<CrawlerManager>.Instance.GetCrawler();
            crawler.OnCompleted += (s, re) =>
            {
                result.Success = true;
                result.Msg = "爬虫抓取成功！耗时:" + re.Milliseconds;
            };
            crawler.OnError += (s, ex) =>
            {
                result.Success = false;
                result.Msg = "爬虫抓取失败:" + ex;
            };

            var rePageStr = string.Empty;
            try
            {
                //启动爬虫
                var rePageStr1 = await crawler.StartAsync(new Uri("http://www.pmtown.com/archives/category/%E6%97%A9%E6%8A%A5"), null);
                var dom = new HtmlParser().Parse(rePageStr1);
                //页面元素
                var newsList = dom.QuerySelectorAll("#list > article").FirstOrDefault();
                if (newsList == null)
                {
                    result.Success = false;
                    result.Msg = "detail页面未找到。";
                    return result;
                }
                var url = newsList.QuerySelectorAll("a").First().GetAttribute("href");
                rePageStr = await crawler.StartAsync(new Uri(url), null);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Msg = "爬虫异常:" + ex;
                return result;
            }

            if (!result.Success) return result;

            try
            {
                var dom = new HtmlParser().Parse(rePageStr);
                var first = NewsFlashItem(dom.FirstElementChild);

                //返回
                result.Success = true;
                result.Result = first;
            }
            catch (JsonException ex)
            {
                result.Success = false;
                result.Msg = "Json解析失败:" + ex;
            }

            try
            {
                var unit = _unitOfWork.GetRepository<CrawlNews>();
                var oldFirst = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.PaomianNewsFrom), x => x.OrderByDescending(p => p.AddTime));

                if (oldFirst == null)
                {
                    unit.Insert(result.Result);
                    await _unitOfWork.SaveChangesAsync();
                    result.Msg = "数据更新成功";
                }
                else
                {
                    var key1 = oldFirst.Title.Length > 32 ? oldFirst.Title.Substring(0, 32) : oldFirst.Title;
                    var key2 = result.Result.Title.Length > 32 ? result.Result.Title.Substring(0, 32) : result.Result.Title;
                    if (oldFirst != null && key1 == key2)
                    {
                        result.Success = false;
                        result.Msg = "当前条目已经是最新";
                    }
                    else
                    {
                        await unit.InsertAsync(result.Result);
                        await _unitOfWork.SaveChangesAsync();
                        result.Msg = "数据更新成功";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Msg = "数据库存储失败:" + ex;
            }

            return result;
        }

        /// <summary>
        /// 将dom元素处理为CrawlNews
        /// </summary>
        /// <returns></returns>
        private CrawlNews NewsFlashItem(IElement element)
        {
            if (element == null) return null;

            //标题
            var title = element.QuerySelector(".uk-article-title").TextContent.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

            //重要等级
            var importantLevel = EnumImportantLevel.Level0;

            //来源
            var from = CrawlNewsFromDef.PaomianNewsFrom;

            //来源地址
            var fromUrl = string.Empty;

            //来源推送时间
            var pushTime = DateTime.Now.ToString("yyyy-MM-dd");

            //内容
            var node = element.QuerySelector(".uk-article");
            node.RemoveChild(node.QuerySelector("h1"));
            node.RemoveChild(node.QuerySelector("ul"));

            var content = element.QuerySelector(".uk-article").TextContent.Replace("\t", "");
            content = content.Substring(content.IndexOf("【"));
            content = content.Replace(content.Substring(content.IndexOf("更早获取早报内容") -14), "");

            //标签，暂时不填
            var tag = "";

            //推送等级，根据重要程度判断
            var pushLevel = EnumPushLevel.Level0;
            if (importantLevel == EnumImportantLevel.Level5 || importantLevel == EnumImportantLevel.Level4)
            {
                pushLevel = EnumPushLevel.Level3;

            }
            else if (importantLevel == EnumImportantLevel.Level3 || importantLevel == EnumImportantLevel.Level2)
            {
                pushLevel = EnumPushLevel.Level2;
            }
            else
            {
                pushLevel = EnumPushLevel.Level1;
            }

            //抓取时间
            var addTime = DateTime.Now;

            var reItem = new CrawlNews
            {
                Title = title,
                ImportantLevel = (int)importantLevel,
                From = from,
                FromUrl = from,
                PushTime = pushTime,
                Content = content,
                Tag = tag,
                PushLevel = (int)pushLevel,
                AddTime = addTime.ToMysqlTime()
            };

            return reItem;
        }

        /// <summary>
        /// 获取最新一条泡面小镇的早报
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlNews> GetLatestNews()
        {
            var reModel = new CrawlNews();

            var unit = _unitOfWork.GetRepository<CrawlNews>();
            reModel = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.PaomianNewsFrom), x => x.OrderByDescending(p => p.AddTime));
            return reModel;
        }
    }
}
