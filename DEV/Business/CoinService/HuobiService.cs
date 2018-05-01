using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Business.CrawlNewsService;
using Business.CrawlNewsService.CoinNewsService;
using Core.Crawler;
using Data.Entity;
using Infrastructure;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Coin
{
    /// <summary>
    /// 火币网服务
    /// </summary>
    public class HuobiService : IhuobiService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HuobiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 法币价格查询
        /// </summary>
        /// <param name="coinId">coin值1为btc,2为usdt</param>
        /// <param name="tradeType">tradeType值1为买0为卖</param>
        /// <returns></returns>
        public CrawlerResult<string> LegalTender(int coinId,int tradeType)
        {
            var result = new CrawlerResult<string>();

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

            //coinId=1，tradeType=1是BTC
            //coinId=2，tradeType=1是USDT
            //coinId=3，tradeType=1是ETH
            var url = "https://otc-api.huobipro.com/v1/otc/trade/list/public?coinId=" + coinId + "&tradeType=" + tradeType + "&currentPage=1&payWay=&country=&merchant=1&online=1&range=0&currPage=1";

            //启动爬虫
            var reJson = HttpUitls.Get(url, "https://otc.huobipro.com/","https://otc.huobipro.com");

            try
            {
                //Json解析
                var x = JsonConvert.DeserializeObject<LegalTenderPage>(reJson);
                var buyFirst = x.data[0];

                if (buyFirst == null)
                {
                    result.Success = true;
                    result.Result = "厉害了！居然没人交易！！！";
                }
                else
                {

                    result.Success = true;
                    result.Result = buyFirst.price.ToString();
                }
            }
            catch (JsonException ex)
            {
                result.Success = false;
                result.Msg = "Json解析失败:" + ex;
            }

            return result;
        }

        
    }
}
