using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.TrackCoin
{
    public class TrackManager: Singleton<TrackManager>
    {
        public Dictionary<string, TrackInfoModel> dict_TrackList;

        public delegate void SendMsgHandler(string from,string msg);
        public SendMsgHandler sendMsgHandler;

        public void Init(SendMsgHandler handler)
        {
            dict_TrackList = new Dictionary<string, TrackInfoModel>();

            sendMsgHandler = handler;

            System.Timers.Timer t = new System.Timers.Timer(1000 * 10);

            t.Elapsed += new System.Timers.ElapsedEventHandler(Ckeck);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        /// <summary>
        /// 加入追踪
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="price"></param>
        public void AddTrack(string coin, TrackInfoModel model)
        {
            if (!dict_TrackList.ContainsKey(coin))
            {
                dict_TrackList.Add(coin, model);

                sendMsgHandler.Invoke(model.fromGroup, "添加好啦，坐等抄底吧~");
            }
            else
            {
                sendMsgHandler.Invoke(model.fromGroup,"已经关注咯，关注价格是"+model.price);
            }
            
        }

        /// <summary>
        /// 移除监听的币
        /// </summary>
        /// <param name="coin"></param>
        public void RemoveTrack(string coin, TrackInfoModel model)
        {
            dict_TrackList.Remove(coin);
            sendMsgHandler.Invoke(model.fromGroup, "不用盯着了，好开心");
        }

        public void Ckeck(object source, System.Timers.ElapsedEventArgs e)
        {
            String[] keyStr = dict_TrackList.Keys.ToArray();
            for (int i = 0; i < keyStr.Length; i++)
            {
                CheckPrice(keyStr[i], dict_TrackList[keyStr[i]]);
            }
        }

        /// <summary>
        /// 检查是否超限
        /// </summary>
        /// <param name="coin"></param>
        private void CheckPrice(string coin, TrackInfoModel model)
        {
            //查价格
            var url = string.Format("https://www.okex.com/api/v1/ticker.do?symbol={0}", coin);
            var json = HttpUitls.Get(url);
            var reModel = JsonHelper.DeserializeJsonToObject<OKTrickModel>(json);

            if (model.isUp)
            {
                //如果价格到了
                if (reModel.ticker.last > model.price)
                {
                    sendMsgHandler.Invoke(model.fromGroup,string.Format("米娜桑！{0}价格大于{1}了！赶紧抄底！", coin, reModel.ticker.last));

                    RemoveTrack(coin, model);
                }

            }
            else
            {
                //如果价格到了
                if (reModel.ticker.last < model.price)
                {
                    sendMsgHandler.Invoke(model.fromGroup,string.Format("米娜桑！{0}价格小于{1}了！赶紧抄底！", coin, reModel.ticker.last));


                    RemoveTrack(coin, model);
                }
            }
        }
    }
}
