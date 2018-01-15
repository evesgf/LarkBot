using Lark.Bot.CQA.Business;
using Lark.Bot.CQA.Handler.TimeJobHandler;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Linq;

namespace Lark.Bot.CQA.Handler.GroupMessageHandler
{
    public class GroupMessageHandler : IGroupMessageHandler
    {
        private readonly ICoinService _iCoinService;
        private readonly ICoinNewsService _iCoinNewsService;
        private readonly ITrackHandler _trackHandler;

        public GroupMessageHandler(ICoinService iCoinService, ICoinNewsService iCoinNewsService, ITrackHandler trackHandler)
        {
            _iCoinService = iCoinService;
            _iCoinNewsService = iCoinNewsService;
            _trackHandler = trackHandler;
        }

        public HandlerResult CheckKeyWord(GroupMessageReceivedContext context)
        {
            var result = new HandlerResult{IsHit = false};

            //场外币价
            if (context.Message.Equals("场外币价"))
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
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Message.Remove(0, 4);

                var re = _iCoinService.GetOKEXCoinPrice(key);

                //回发
                result.IsHit = true;
                result.Msg = re;
            }

            //看币价
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("看币价 "))
            {
                string key = context.Message.Remove(0, 4);

                var re = _iCoinService.GetMyTokenPrice(key);

                //回发
                result.IsHit = true;
                result.Msg =re;
            }

            //币圈消息
            if (context.Message.Equals("币圈消息"))
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

            //开启监听 okex btc_usdt > 1000
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("开启监听"))
            {
                string[] keys = context.Message.Split(' ');
                if (keys.Count() != 5)
                {
                    result.Msg = "指令输入错误";
                    return result;
                }

                var model = new TrackPriceModel
                {
                    //fromQQ = context.FromQq,
                    fromGroup = context.FromGroup,
                    msgType = Enum_MsgType.GroupMsg,
                    exchange = keys[1],
                    coin = keys[2],
                    isUp = keys[3].Equals(">"),
                    price = Convert.ToDecimal(keys[4])
                };

                if (_trackHandler.StartTrackCoinPrice( model))
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "监听开启！";
                }
                else
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "监听程序BUG了，快召唤程序猿~";
                }
            }

            //关闭监听 okex btc_usdt
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("关闭监听"))
            {
                string[] keys = context.Message.Split(' ');
                if (keys.Count() != 3)
                {
                    result.Msg = "指令输入错误";
                    return result;
                }

                var model = new TrackPriceModel
                {
                    //fromQQ = context.FromQq,
                    fromGroup = context.FromGroup,
                    msgType = Enum_MsgType.GroupMsg,
                    exchange = keys[1],
                    coin = keys[2]
                };

                if (_trackHandler.StopTrackCoinPrice( model))
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "好累！终于不用盯着了";
                }
                else
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "监听程序BUG了，快召唤程序猿~";
                }
            }

            return result;
        }
    }
}
