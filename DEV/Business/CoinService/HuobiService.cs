﻿using AngleSharp.Parser.Html;
using Core.Crawler;
using Infrastructure;
using Infrastructure.Common;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Business.Coin
{
    /// <summary>
    /// 火币网服务
    /// </summary>
    public class HuobiService : IhuobiService
    {
        /// <summary>
        /// 法币价格查询
        /// </summary>
        /// <param name="coinId">coin值1为btc,2为usdt</param>
        /// <param name="tradeType">tradeType值1为买0为卖</param>
        /// <returns></returns>
        public async Task<CrawlerResult<string>> LegalTender(int coinId,int tradeType)
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

            //启动爬虫
            var reJson =await crawler.StartAsync(new Uri("https://api-otc.huobi.pro/v1/otc/trade/list/public?coinId="+ coinId + "&tradeType="+ tradeType + "&currentPage=1&payWay=&country=&merchant=0&online=1&range=0"), null);

            if (!result.Success) return result;

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
