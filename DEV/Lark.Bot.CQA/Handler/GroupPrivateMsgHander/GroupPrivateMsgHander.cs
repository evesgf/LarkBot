using Newbe.Mahua.MahuaEvents;

namespace Lark.Bot.CQA.Handler.GroupPrivateMsgHander
{
    public class GroupPrivateMsgHander: IGroupPrivateMsgHander
    {
        //private readonly ITrackHandler _trackHandler;

        //public GroupPrivateMsgHander(ITrackHandler trackHandler)
        //{
        //    _trackHandler = trackHandler;
        //}

        /// <summary>
        /// 传入关键词判断
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        HandlerResult IGroupPrivateMsgHander.CheckKeyWord(PrivateMessageFromGroupReceivedContext context)
        {
            var result = new HandlerResult { IsHit = false };

            ////开启监听 okex btc_usdt > 1000
            //if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("开启监听"))
            //{
            //    string[] keys = context.Message.Split(' ');
            //    if (keys.Count() != 5)
            //    {
            //        result.Msg = "指令输入错误";
            //        return result;
            //    }

            //    var model = new TrackPriceModel
            //    {
            //        fromQQ = context.FromQq,
            //        fromGroup = context.FromGroup,
            //        msgType = Enum_MsgType.PrivateGroup,
            //        exchange = keys[1],
            //        coin = keys[2],
            //        isUp = keys[3].Equals(">"),
            //        price = Convert.ToDecimal(keys[4])
            //    };

            //    if (_trackHandler.StartTrackCoinPrice(context.FromQq, model))
            //    {
            //        //回发
            //        result.IsHit = true;
            //        result.Msg = "监听开启！";
            //    }
            //    else
            //    {
            //        //回发
            //        result.IsHit = true;
            //        result.Msg = "监听程序BUG了，快召唤程序猿~";
            //    }
            //}

            ////关闭监听 okex btc_usdt
            //if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("关闭监听"))
            //{
            //    string[] keys = context.Message.Split(' ');
            //    if (keys.Count() != 3)
            //    {
            //        result.Msg = "指令输入错误";
            //        return result;
            //    }

            //    var model = new TrackPriceModel
            //    {
            //        fromQQ = context.FromQq,
            //        fromGroup = context.FromGroup,
            //        msgType = Enum_MsgType.PrivateGroup,
            //        exchange = keys[1],
            //        coin = keys[2]
            //    };

            //    if (_trackHandler.StopTrackCoinPrice(context.FromQq, model))
            //    {
            //        //回发
            //        result.IsHit = true;
            //        result.Msg = "好累！终于不用盯着了";
            //    }
            //    else
            //    {
            //        //回发
            //        result.IsHit = true;
            //        result.Msg = "监听程序BUG了，快召唤程序猿~";
            //    }
            //}

            return result;
        }
    }
}
