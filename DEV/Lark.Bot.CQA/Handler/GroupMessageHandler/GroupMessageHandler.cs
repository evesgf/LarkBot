using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lark.Bot.CQA.Business;
using Lark.Bot.CQA.Handler.TimeJobHandler;

namespace Lark.Bot.CQA.Handler.GroupMessageHandler
{
    public class GroupMessageHandler : IGroupMessageHandler
    {
        private readonly ICoinService _iCoinService;
        private readonly ICoinNewsService _iCoinNewsService;

        public GroupMessageHandler(ICoinService iCoinService, ICoinNewsService iCoinNewsService)
        {
            _iCoinService = iCoinService;
            _iCoinNewsService = iCoinNewsService;
        }

        public HandlerResult CheckKeyWord(string context)
        {
            var result = new HandlerResult{IsHit = false};

            //场外币价
            if (context.Equals("场外币价"))
            {
                result.Msg = "【场外币价】";

                var re2 = _iCoinService.OTCPrice().Data;
                foreach (var str in re2)
                {
                    result.Msg += str + "\n";
                }

                //回发
                result.IsHit = true;
            }

            //查币价
            if (context.Length > 4 && context.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Remove(0, 4);

                var re = _iCoinService.GetOKEXCoinPrice(key);

                //回发
                result.IsHit = true;
                result.Msg = re;
            }

            //看币价
            if (context.Length > 4 && context.Substring(0, 4).Equals("看币价 "))
            {
                string key = context.Remove(0, 4);

                var re = _iCoinService.GetMyTokenPrice(key);

                //回发
                result.IsHit = true;
                result.Msg =re;
            }

            //币圈消息
            if (context.Equals("币圈消息"))
            {
                var re = _iCoinNewsService.RequestBiQuanApi();
                foreach (var str in re)
                {
                    result.Msg += str + "\n";
                }

                result.Msg += "【场外币价】";
                var re2 = _iCoinService.OTCPrice().Data;
                foreach (var str in re2)
                {
                    result.Msg += str + "\n";
                }

                //查询币圈
                result.Msg += _iCoinService.GetOKEXCoinPrice("btc_usdt");

                //回发
                result.IsHit = true;
            }

            //if (context.Equals("开启币圈消息推送"))
            //{
            //    var re=_timeJobHandler.StartPushNews("");

            //    //回发
            //    result.IsHit = true;
            //    result.Msg = re.ToString();
            //}

            return result;
        }
    }
}
