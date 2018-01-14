using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lark.Bot.CQA.Business;

namespace Lark.Bot.CQA.Handler.GroupMessageHandler
{
    public class GroupMessageHandler : IHandler
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
                var re = _iCoinService.OTCPrice();

                //回发
                result.IsHit = true;
                result.Msg = "场外币价:" + re.Data[0];
            }

            //查币价
            if (context.Length > 4 && context.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Remove(0, 4);

                var re = _iCoinService.GetOKEXCoinPrice(key);

                //回发
                result.IsHit = true;
                result.Msg = key+":" + re;
            }

            //看币价
            if (context.Length > 4 && context.Substring(0, 4).Equals("看币价 "))
            {
                string key = context.Remove(0, 4);

                var re = _iCoinService.GetMyTokenPrice(key);

                //回发
                result.IsHit = true;
                result.Msg = key + ":" + re;
            }

            //币圈消息
            if (context.Equals("币圈消息"))
            {
                //查询币圈
                var re = _iCoinNewsService.RequestBiQuanApi() + "\n" + _iCoinService.OTCPrice() + "\n" + _iCoinService.GetOKEXCoinPrice("btc_usdt");

                //回发
                result.IsHit = true;
                result.Msg =re;
            }

            return result;
        }
    }
}
