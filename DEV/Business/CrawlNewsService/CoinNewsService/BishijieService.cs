using AngleSharp.Parser.Html;
using Core.Crawler;
using Data.Entity;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Business.CrawlNewsService.CoinNewsService
{
    public class BishijieService : IBshijieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BishijieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 更新币世界推送的快讯
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
            //http://www.bishijie.com/api/news/?size=1&timestamp=1515751702
            //前端爬不出来，直接读接口
            var rePageStr = await crawler.StartAsync(new Uri("http://www.bishijie.com/api/news/?size=1&timestamp=" + TimeHelper.ConvertToTimeStamp(DateTime.Now)), null);

            if (!result.Success) return result;

            try
            {
                var first = NewsFlashItem(rePageStr);

                //返回
                result.Success = true;
                result.Result = first;
            }
            catch (JsonException ex)
            {
                result.Success = false;
                result.Msg = "字符串解析失败:" + ex;
            }

            try
            {
                var unit = _unitOfWork.GetRepository<CrawlNews>();
                var oldFirst = await unit.GetFirstOrDefaultAsync(x => x, x => x.From.Equals(CrawlNewsFromDef.BishijieFlashFrom), x => x.OrderByDescending(p => p.AddTime));

                if (oldFirst != null && oldFirst.PushTime == result.Result.PushTime)
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
        /// 将字符串处理为CrawlNews
        /// </summary>
        /// <returns></returns>
        private CrawlNews NewsFlashItem(string str)
        {
            if (str == null) return null;

            //标题
            var title = str.Split(new string[] { "\"content\":\"", ",\"source" }, StringSplitOptions.RemoveEmptyEntries)[1];

            //重要等级,Rank值
            var importantLevel = EnumImportantLevel.Level0;
            if (Convert.ToInt32(str.Substring(str.IndexOf("\"rank\":")+7, 1))==1)
            {
                importantLevel = EnumImportantLevel.Level5;
            }

            //来源
            var from = CrawlNewsFromDef.BishijieFlashFrom;

            //来源地址，快讯类型没必要填
            var fromUrl = string.Empty;

            //来源推送时间
            var pushTime = DateTime.Now.ToString("yyyy-MM-dd");
            var longTime = str.Split(new string[] { "\"issue_time\":", ",\"rank\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
            pushTime += " " + TimeHelper.GetTime(long.Parse(longTime)).ToString("T");

            //标题长度不够，内容再存一遍
            var content = str.Split(new string[] { "\"content\":\"", ",\"source" }, StringSplitOptions.RemoveEmptyEntries)[1];
            var link = str.Split(new string[] { "\"link\":\"", "\",\"issue_time\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
            if (string.IsNullOrEmpty(link))
            {
                content = link;
            }

            //标签，暂时不填
            var tag = "";

            //推送等级，根据重要程度判断
            var pushLevel = EnumPushLevel.Level0;
            if (importantLevel == EnumImportantLevel.Level5)
            {
                pushLevel = EnumPushLevel.Level3;

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

            reModel = await unit.GetFirstOrDefaultAsync(x=>x,x=>x.From.Equals(CrawlNewsFromDef.BishijieFlashFrom),x=>x.OrderByDescending(p=>p.AddTime));

            return reModel;
        }
    }
}
