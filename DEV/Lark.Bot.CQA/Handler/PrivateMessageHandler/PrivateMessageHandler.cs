using Lark.Bot.CQA.Business;
using Lark.Bot.CQA.Handler.TimeJobHandler;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler.PrivateMessageHandler
{
    public class PrivateMessageHandler: IPrivateMessageHandler
    {
        private readonly ICoinService _iCoinService;
        private readonly ICoinNewsService _iCoinNewsService;
        private readonly ITrackHandler _trackHandler;

        public PrivateMessageHandler(ICoinService iCoinService, ICoinNewsService iCoinNewsService, ITrackHandler trackHandler)
        {
            _iCoinService = iCoinService;
            _iCoinNewsService = iCoinNewsService;
            _trackHandler = trackHandler;
        }

        public HandlerResult CheckKeyWord(PrivateMessageFromFriendReceivedContext context)
        {
            var result = new HandlerResult { IsHit = false };

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
                    fromQQ = context.FromQq,
                    //fromGroup = context.FromGroup,
                    msgType = Enum_MsgType.PrivateMsg,
                    exchange = keys[1],
                    coin = keys[2],
                    isUp = keys[3].Equals(">"),
                    price = Convert.ToDecimal(keys[4])
                };

                if (_trackHandler.StartTrackCoinPrice(model))
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
                    fromQQ = context.FromQq,
                    //fromGroup = context.FromGroup,
                    msgType = Enum_MsgType.PrivateMsg,
                    exchange = keys[1],
                    coin = keys[2]
                };

                if (_trackHandler.StopTrackCoinPrice(model))
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

            //监听列表 okex btc_usdt
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("监听列表"))
            {
                string[] keys = context.Message.Split(' ');
                if (keys.Count() != 3)
                {
                    result.Msg = "指令输入错误";
                    return result;
                }

                var model = new TrackPriceModel
                {
                    fromQQ = context.FromQq,
                    //fromGroup = context.FromGroup,
                    msgType = Enum_MsgType.PrivateMsg,
                    exchange = keys[1],
                    coin = keys[2]
                };

                var list = _trackHandler.GetTrackList(model);
                if (list != null)
                {
                    var remsg = string.Empty;
                    foreach (var item in list)
                    {
                        remsg += item.coin + " " + item.isUp + " " + item.price + "|";
                    }

                    //回发
                    result.IsHit = true;
                    result.Msg = remsg;
                }
                else
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "什么都没关注呢~关注一把看看呗";
                }
            }

            return result;
        }
    }
}
