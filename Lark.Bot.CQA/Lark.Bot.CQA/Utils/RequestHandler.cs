using Lark.Bot.CQA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RequestHandler
{
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
    public static string RequestBiQuanApi()
    {
        try
        {
            string typeBcURL = "http://newsserver.evesgf.com/api/BitNews/GetNewestPaper";

            return HttpUitls.Get(typeBcURL);
        }
        catch (Exception e)
        {
            return e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!";
        }
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
    #endregion
}
