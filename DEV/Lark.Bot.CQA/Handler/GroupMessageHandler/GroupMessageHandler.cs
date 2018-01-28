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

            //查看所有的输入口令
            if (context.Message.Equals("查看所有指令"))
            {
                result.Msg += "【场外币价】 | ";
                result.Msg += "【查币价 btc_usdt】 | ";
                result.Msg += "【看币价 btc】 | ";
                result.Msg += "【币圈消息】 | ";
                result.Msg += "【开启监听 okex btc_usdt > 1000】 | ";
                result.Msg += "【关闭监听 okex btc_usdt】 | ";
                result.Msg += "【监听列表 okex】 | ";
                result.Msg += "【okex涨幅】 | ";
                result.Msg += "【okex跌幅】 | ";
                result.Msg += "【早报】 | ";

                result.IsHit = true;
                
            }


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
                    //回发
                    result.IsHit = true;
                    result.Msg = "指令输入错误，指令格式为【开启监听 okex btc_usdt > 1000】";
                }
                else
                {
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
            }

            //关闭监听 okex btc_usdt
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("关闭监听"))
            {
                string[] keys = context.Message.Split(' ');
                if (keys.Count() != 3)
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "指令输入错误，指令格式为【关闭监听 okex btc_usdt】";
                }
                else
                {
                    var model = new TrackPriceModel
                    {
                        //fromQQ = context.FromQq,
                        fromGroup = context.FromGroup,
                        msgType = Enum_MsgType.GroupMsg,
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
            }

            //监听列表 okex
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("监听列表"))
            {
                string[] keys = context.Message.Split(' ');
                if (keys.Count() != 3)
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "指令输入错误,指令格式为【监听列表 okex】";
                }
                else
                {
                    var model = new TrackPriceModel
                    {
                        //fromQQ = context.FromQq,
                        fromGroup = context.FromGroup,
                        msgType = Enum_MsgType.GroupMsg,
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
            }

            //okex涨幅
            if (context.Message.Equals("okex涨幅"))
            {
                var str=_iCoinService.GetOkexTopTracks();

                if (!string.IsNullOrEmpty(str))
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = str;
                    
                }
                else
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "发生了什么？怎么什么都没有？？";
                    
                }
            }

            //okex涨幅
            if (context.Message.Equals("okex跌幅"))
            {
                var str = _iCoinService.GetOkexBottomTracks();

                if (!string.IsNullOrEmpty(str))
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = str;
                    
                }
                else
                {
                    //回发
                    result.IsHit = true;
                    result.Msg = "发生了什么？怎么什么都没有？？";
                    
                }
            }

            return result;
        }
    }
}
