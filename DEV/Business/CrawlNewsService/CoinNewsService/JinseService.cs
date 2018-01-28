using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Core.Crawler;
using Data.Entity;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.CoinNewsService
{
    public class JinseService:IJinseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JinseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 更新金色财经推送的快讯
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlerResult<CrawlNews>> UpdatePushNewsFlash()
        {
            var result=new CrawlerResult<CrawlNews>();

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
                rePageStr =await crawler.StartAsync(new Uri("http://www.jinse.com/lives"), null);
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
                var newsList = dom.QuerySelectorAll(".clearfix").FirstOrDefault();
                var first=NewsFlashItem(newsList);

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
                var oldFirst =await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.JinseFlashFrom), x => x.OrderByDescending(p => p.AddTime));

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
            var title = element.QuerySelector(".live-info").TextContent.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            //利好利空
            var bull = element.QuerySelector(".live-index-pull-bear").TextContent.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("+1", "");

            title += bull;

            //重要等级
            var importantLevel = EnumImportantLevel.Level0;
            var star = element.QuerySelector("p.star-wrap-new");
            if (star!=null && star.HasChildNodes)
            {
                importantLevel =(EnumImportantLevel) element.QuerySelector("span.star-wrap-new").QuerySelectorAll("span.star-bright").Count();
            }

            //来源
            var from = CrawlNewsFromDef.JinseFlashFrom;

            //来源地址，快讯类型没必要填
            var fromUrl = string.Empty;

            //来源推送时间
            var pushTime = DateTime.Now.ToString("yyyy-MM-dd");
            pushTime += " " + element.QuerySelector(".live-time").TextContent+":00";

            //标题长度不够，内容再存一遍
            var content = element.QuerySelector(".live-info").TextContent.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            content += bull;

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
        /// 获取最新一条快讯
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlNews> GetLatestNewsFlash()
        {
            var reModel = new CrawlNews();

            var unit = _unitOfWork.GetRepository<CrawlNews>();
            reModel = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.JinseFlashFrom), x => x.OrderByDescending(p => p.AddTime));
            return reModel;
        }
    }
}
