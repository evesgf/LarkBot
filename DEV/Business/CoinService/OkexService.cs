using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Business.CrawlNewsService;
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

namespace Business.CoinService
{
    public class OkexService: IOkexService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OkexService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 更新OKEX的公告
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlerResult<CrawlNews>> UpdateNoticeFlash()
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
                rePageStr = await crawler.StartAsync(new Uri("https://support.okex.com/hc/zh-cn/categories/115000275131-%E5%85%AC%E5%91%8A%E4%B8%AD%E5%BF%83"), null);
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

                //页面元素
                var newsList = dom.QuerySelectorAll(".article-list-item").FirstOrDefault();
                var first = NewsFlashItem(newsList);

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
                var oldFirst = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.OkexNoticeFrom), x => x.OrderByDescending(p => p.AddTime));


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
                        unit.Insert(result.Result);
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
            var title = element.TextContent.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""); 

            //重要等级
            var importantLevel = EnumImportantLevel.Level5;
            var star = element.QuerySelector("p.star-wrap-new");

            //来源
            var from = CrawlNewsFromDef.OkexNoticeFrom;

            //来源地址，快讯类型没必要填
            var fromUrl = "https://support.okex.com" + element.QuerySelector("a").GetAttribute("href");

            //来源推送时间
            var pushTime = DateTime.Now.ToString("yyyy-MM-dd");
            //pushTime += " " + element.QuerySelector(".live-time").TextContent + ":00";

            //标题长度不够，内容再存一遍
            var content = title;

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
        /// 获取最新一条公告
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlNews> GetLatestNotice()
        {
            var reModel = new CrawlNews();

            var unit = _unitOfWork.GetRepository<CrawlNews>();
            reModel = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.OkexNoticeFrom), x => x.OrderByDescending(p => p.AddTime));
            return reModel;
        }
    }
}
