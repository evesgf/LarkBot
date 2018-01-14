using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.Coin
{
    public class CoinService:ICoinService
    {
        /// <summary>
        /// 场外币价
        /// </summary>
        /// <returns></returns>
        public string OTCPrice()
        {
            var re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

            try
            {
                string typeBcURL = "http://newsserver.evesgf.com/api/BitNews/GetOffSitePrice";

                re = HttpUitls.Get(typeBcURL);

                string typeBcUrl2 = "http://larkbot.evesgf.com/api/Coin/OTCPrice";
                var re2 = JsonHelper.DeserializeJsonToObject<OTCPrice>(HttpUitls.Get(typeBcUrl2));
                if (re2.Access)
                {
                    foreach (var item in re2.Data)
                    {
                        re += "," + item;
                    }
                }

                return re;
            }
            catch (Exception e)
            {
                return e.ToString() + "\n锅咩呐~咱的灵魂程序猿又写了个Bug，等我召唤主人来修复吧~";
            }
        }

        #region 币价告警


        /// <summary>
        /// 开启追踪
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <param name="coinName">币名</param>
        /// <param name="price">价格</param>
        /// <param name="isUp">超过还是低于</param>
        public void StartTrace(string exchange, string coinName, decimal price, bool isUp)
        {

        }

        /// <summary>
        /// 停止追踪
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <param name="coinName">币名</param>
        public void StopTrace(string exchange, string coinName)
        {

        }
        #endregion
    }
}
