using Lark.Bot.CQA.Business;
using Newbe.Mahua;
using System.Collections.Generic;
using System.Timers;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    public class TrackHandler: ITrackHandler
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly ICoinService _coinService;

        public TrackHandler(IMahuaApi mahuaApi,ICoinService coinService)
        {
            _mahuaApi = mahuaApi;
            _coinService = coinService;
        }

        #region 币价监听
        private List<TrackPriceModel> trackList;

        /// <summary>
        /// 开启币价监听追踪
        /// model中fromQQ和fromGroup都填的时候是群成员私聊
        /// </summary>
        /// <param name="fromQQ"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool StartTrackCoinPrice(TrackPriceModel model)
        {
            //私聊添加
            if (model.msgType == Enum_MsgType.PrivateMsg)
            {
                trackList.Add(model);
            }
            if (model.msgType == Enum_MsgType.GroupMsg)
            {
                trackList.Add(model);
            }
            if (model.msgType == Enum_MsgType.PrivateGroup)
            {
                trackList.Add(model);
            }

            return true;
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="fromQQ"></param>
        /// <param name="fromGroup"></param>
        /// <returns></returns>
        public bool StopTrackCoinPrice(TrackPriceModel model)
        {
            var m = trackList.Find(x => x.fromQQ == model.fromQQ && x.fromGroup == model.fromGroup && x.coin == model.coin);
            if (m != null)
            {
                trackList.Remove(m);

                return true;
            }

            return false;
        }

        Timer trackTime = new Timer(1000 * 10 * 1);
        public void StartLoop()
        {
            trackList = new List<TrackPriceModel>();

            trackTime.Elapsed += new ElapsedEventHandler(LoopBody);
            trackTime.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            trackTime.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        public void LoopBody(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < trackList.Count; i++)
            {
                if (trackList[i].exchange.Equals("okex"))
                {
                    var keys = trackList[i].coin.Split('_');

                    var url = "https://www.okex.com/api/v1/ticker.do?symbol=" + trackList[i].coin;
                    var market = JsonHelper.DeserializeJsonToObject<Market>(HttpUitls.Get(url));

                    var url2 = "https://api.huobi.pro/market/trade?symbol=" + keys[0] + keys[1];
                    var reModel = JsonHelper.DeserializeJsonToObject<HuobiResult>(HttpUitls.Get(url));

                    if (market == null) return;

                    if (trackList[i].isUp)
                    {
                        if (market.ticker.last > trackList[i].price)
                        {
                            SendTrackMessage(trackList[i], market.ticker.last.ToString());
                            StopTrackCoinPrice(trackList[i]);
                        }
                    }
                    else
                    {
                        if (market.ticker.last < trackList[i].price)
                        {
                            SendTrackMessage(trackList[i], market.ticker.last.ToString());
                            StopTrackCoinPrice(trackList[i]);
                        }
                    }
                }

                if (trackList[i].exchange.Equals("火币"))
                {
                    var keys = trackList[i].coin.Split('_');

                    var url2 = "https://api.huobi.pro/market/trade?symbol=" + keys[0] + keys[1];
                    var reModel = JsonHelper.DeserializeJsonToObject<HuobiResult>(HttpUitls.Get(url2));

                    if (reModel == null) return;

                    if (trackList[i].isUp)
                    {
                        if (reModel.tick.data[0].price > trackList[i].price)
                        {
                            SendTrackMessage(trackList[i], reModel.tick.data[0].price.ToString());
                            StopTrackCoinPrice(trackList[i]);
                        }
                    }
                    else
                    {
                        if (reModel.tick.data[0].price < trackList[i].price)
                        {
                            SendTrackMessage(trackList[i], reModel.tick.data[0].price.ToString());
                            StopTrackCoinPrice(trackList[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 回发消息
        /// </summary>
        /// <param name="model"></param>
        private void SendTrackMessage(TrackPriceModel model, string price)
        {
            if (model.msgType == Enum_MsgType.PrivateMsg)
            {
                _mahuaApi.SendPrivateMessage(model.fromQQ,"米娜桑！:"+ model.coin+"当前价格为" + price+"，还在等什么？！");
            }
            if (model.msgType == Enum_MsgType.GroupMsg)
            {
                _mahuaApi.SendGroupMessage(model.fromGroup, "米娜桑！:" + model.coin + "当前价格为" + price+ "，还在等什么？！");
            }
            if (model.msgType == Enum_MsgType.PrivateGroup)
            {
                _mahuaApi.SendPrivateMessage(model.fromQQ, "米娜桑！:" + model.coin + "当前价格为" + price + "，还在等什么？！");
            }
        }
        #endregion
    }

    /// <summary>
    /// 追踪价格模型
    /// </summary>
    public class TrackPriceModel
    {
        /// <summary>
        /// 来源QQ
        /// </summary>
        public string fromQQ { get; set; }
        /// <summary>
        /// 来源组
        /// </summary>
        public string fromGroup { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public Enum_MsgType msgType { get; set; }
        /// <summary>
        /// 交易所
        /// </summary>
        public string exchange { get; set; }
        /// <summary>
        /// 币名 btc_usdt
        /// </summary>
        public string coin { get; set; }
        /// <summary>
        /// 大于还是小于
        /// </summary>
        public bool isUp { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
    }
}
