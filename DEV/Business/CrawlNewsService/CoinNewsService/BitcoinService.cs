using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Infrastructure.Common;
using AngleSharp.Dom;
using Newtonsoft.Json;
using AngleSharp.Parser.Html;
using Core.Crawler;

namespace Business.CrawlNewsService.CoinNewsService
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BitcoinService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取最新一条快讯
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlNews> GetLatestNewsFlash()
        {
            var reModel = new CrawlNews();

            var unit = _unitOfWork.GetRepository<CrawlNews>();
            reModel = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.BitcoinNewsFrom), x => x.OrderByDescending(p => p.AddTime));
            return reModel;
        }

        /// <summary>
        /// 更新Bitcoin推送的快讯
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlerResult<CrawlNews>> UpdatePushNewsFlash()
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

            //启动爬虫
            var rePageStr =await crawler.StartAsync(new Uri("https://news.bitcoin.com/press-releases/"), null);

            if (!result.Success) return result;

            try
            {
                var dom = new HtmlParser().Parse(rePageStr);

                //页面元素
                var info = dom.QuerySelectorAll(".latest-container").FirstOrDefault();

                var first = NewsFlashItem(info);

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
                var oldFirst = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.BitcoinNewsFrom), x => x.OrderByDescending(p => p.AddTime));

                //页面特殊性质，不能比较抓取时间
                if (oldFirst != null && oldFirst.Title == result.Result.Title)
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
            var title = element.QuerySelector(".entry-title").TextContent.Replace("\t", "").Replace("\r", "").Replace("  ", "").Replace("\n", "");

            //重要等级
            var importantLevel = EnumImportantLevel.Level0;
            //var star = element.QuerySelector("p.star-wrap-new");
            //if (star != null && star.HasChildNodes)
            //{
            //    importantLevel = (EnumImportantLevel)element.QuerySelector("span.star-wrap-new").QuerySelectorAll("span.star-bright").Count();
            //}

            //来源
            var from = CrawlNewsFromDef.BitcoinNewsFrom;

            //来源地址，快讯类型没必要填
            var fromUrl = element.QuerySelector(".td-bonus-excerpt").GetAttribute("href");

            //来源推送时间
            var pushTime = DateTime.Now.ToMysqlTime();
            var keys = element.QuerySelector(".latest-left").TextContent.Replace("\t", "").Replace("\r", "").Replace("  ", "").Replace("\n", "").Split(" ");

            if (keys.Length > 2 && keys[1] != null)
            {
                if (keys[1].Equals("mins"))    //当时间相差为分
                {
                    pushTime = DateTime.Now.AddMinutes(-Convert.ToInt32(keys[0])).ToMysqlTime();
                }
                if (keys[1].Equals("hours"))    //当时间相差为小时
                {
                    pushTime = DateTime.Now.AddHours(-Convert.ToInt32(keys[0])).ToMysqlTime();
                }
                if (keys[1].Equals("day"))    //当时间相差为天
                {
                    pushTime = DateTime.Now.AddDays(-Convert.ToInt32(keys[0])).ToMysqlTime();
                }
            }

            var content = element.QuerySelector(".td-bonus-excerpt").TextContent.Replace("\t", "").Replace("\r", "").Replace("  ", "").Replace("\n", ""); ;

            //标签，暂时不填
            var tag = "";

            //推送等级，根据重要程度判断
            var pushLevel = EnumPushLevel.Level0;
            //if (importantLevel == EnumImportantLevel.Level5 || importantLevel == EnumImportantLevel.Level4)
            //{
            //    pushLevel = EnumPushLevel.Level3;

            //}
            //else if (importantLevel == EnumImportantLevel.Level3 || importantLevel == EnumImportantLevel.Level2)
            //{
            //    pushLevel = EnumPushLevel.Level2;
            //}
            //else
            //{
            //    pushLevel = EnumPushLevel.Level1;
            //}

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
    }
}
