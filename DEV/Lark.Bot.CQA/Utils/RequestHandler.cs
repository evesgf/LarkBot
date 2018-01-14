using Lark.Bot.CQA.Models;
using Lark.Bot.CQA.Modules.Coin;
using Lark.Bot.CQA.Modules.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RequestHandler
{
    private const string url = "http://larkbot.evesgf.com";

    #region 新闻
    /// <summary>
    /// 向LarkNews请求早报，这个数据源周末不干活
    /// </summary>
    /// <returns></returns>
    public static string RequestZaoBaoApi()
    {
        string typeBcURL = "http://newsserver.evesgf.com/api/Home/fristpaper";

        return HttpUitls.Get(typeBcURL);
    }

    /// <summary>
    /// 从LarkNews获取最近一条早报
    /// </summary>
    /// <returns></returns>
    public static string RequestLastZaoBaoApi()
    {
        try
        {
            string typeBcURL = "http://newsserver.evesgf.com/api/Home/LastPaper";

            return HttpUitls.Get(typeBcURL);

        }
        catch (Exception e)
        {
            return e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!";
        }
    }
    #endregion

    #region 币圈无大事
    /// <summary>
    /// 从LarkNews获取币圈消息
    /// </summary>
    /// <returns></returns>
    public static string[] RequestBiQuanApi()
    {
        string[] reStr;

        try
        {
            var jinseLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetJinseLatestNewsFlash"));
            var bishijieLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetBishijieLatestNewsFlash"));
            var bitcoinLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetBitcoinLatestNewsFlash"));

            reStr = new string[] { jinseLatestNewsFlash.Data.Content, bishijieLatestNewsFlash.Data.Content, bitcoinLatestNewsFlash.Data.Content };

        }
        catch (Exception e)
        {
            reStr = new string[] { e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!" };
        }

        return reStr;
    }

    /// <summary>
    /// 获取okex币价
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetBitPrice(string key)
    {
        try
        {
            string typeBcURL = "https://www.okex.com/api/v1/ticker.do?symbol=" + key;

            Market market = JsonHelper.DeserializeJsonToObject<Market>(HttpUitls.Get(typeBcURL));

            string re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

            if (market.date != null && market.ticker != null)
            {
                re = key + " 买一:" + market.ticker.buy + " 最高:" + market.ticker.high + " 最新成交:" + market.ticker.last + " 最低:" + market.ticker.low + " 卖一:" + market.ticker.sell + " 24h成交:" + market.ticker.vol;
            }

            return re;
        }
        catch (Exception e)
        {
            return e.ToString() + "\n锅咩呐~咱的灵魂程序猿又写了个Bug，等我召唤主人来修复吧~";
        }
    }

    public static string GetBitPrice2(string key)
    {
        string re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

        try
        {
            string typeBcURL = "http://api.lb.mytoken.org/currency/filterlist";

            string data = "keyword=" + key + "&market_id=1303&timestamp=1514259573450&code=7c976da9f6797a8a85a73bf30562c6f6&platform=m&";

            var r = HttpUitls.Post(typeBcURL, data, "http://app.mytoken.io/");
            var a = JsonHelper.DeserializeJsonToObject<MTSelectBit>(r);
            var x = a.data.list.FirstOrDefault();

            string listUrl = "http://api.lb.mytoken.org/ticker/currencyexchangelist?currency_id="+x.currency_id+"&page=1&timestamp=1514266516737&code=d690c07d97539898d1387f7cf112d172&platform=m&";

            var rex = HttpUitls.Get(listUrl);
            var b = JsonHelper.DeserializeJsonToObject<MTSelectBit>(rex);
            if (b != null && b.data.list!=null)
            {

                MTBit bithumb = b.data.list.Where(asx => asx.market_name.Equals("Bithumb")).FirstOrDefault();
                MTBit coincheck = b.data.list.Where(asx => asx.market_name.Equals("Coincheck")).FirstOrDefault();
                MTBit bitfinex = b.data.list.Where(asx => asx.market_name.Equals("Bitfinex")).FirstOrDefault();
                MTBit gdax = b.data.list.Where(asx => asx.market_name.Equals("GDAX")).FirstOrDefault();
                MTBit okex = b.data.list.Where(asx => asx.market_name.Equals("OKEx")).FirstOrDefault();
                MTBit bitstamp = b.data.list.Where(asx => asx.market_name.Equals("Bitstamp")).FirstOrDefault();

                re = key;
                re += "\nBithumb: " + FormartMTBit(bithumb) + " | Coincheck: " + FormartMTBit(coincheck) + " | Bitfinex: " + FormartMTBit(bitfinex);
                re+= "\nGDAX: " + FormartMTBit(gdax) + " | OKEx: " + FormartMTBit(okex) + " | Bitstamp: " + FormartMTBit(bitstamp);

                return re;
            };

            return "没数据啊";
        }
        catch (Exception e)
        {
            return e.ToString() + "\n锅咩呐~咱的灵魂程序猿又写了个Bug，等我召唤主人来修复吧~";
        }
    }

    public static string FormartMTBit(MTBit bit)
    {
        if (bit == null)
        {
            return "咱不卖";
        }
        return bit.price_usd.ToString("f4")+"$ ";
    }

    /// <summary>
    /// 场外币价
    /// </summary>
    /// <returns></returns>
    public static string OTCPrice()
    {
        var re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

        try
        {
            string typeBcURL = "http://newsserver.evesgf.com/api/BitNews/GetOffSitePrice";

            re= "场外币价："+HttpUitls.Get(typeBcURL);

            string typeBcUrl2 = "http://larkbot.evesgf.com/api/Coin/OTCPrice";
            re +=","+JsonHelper.DeserializeJsonToObject<OTCPrice>(HttpUitls.Get(typeBcUrl2)).Data[0];

            return re;
        }
        catch (Exception e)
        {
            return e.ToString() + "\n锅咩呐~咱的灵魂程序猿又写了个Bug，等我召唤主人来修复吧~";
        }
    }
    #endregion
}
