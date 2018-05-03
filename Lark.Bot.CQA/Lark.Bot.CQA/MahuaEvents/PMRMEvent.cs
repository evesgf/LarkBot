using Lark.Bot.CQA.Uitls.Config;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.MahuaEvents
{
    public class PMRMEvent : IPrivateMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public PMRMEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessPrivateMessage(PrivateMessageReceivedContext context)
        {
            //转发 群 群号 消息
            if (context.FromQq.Equals(ConfigManager.larkBotConfig.AdminQQ))
            {
                var keys = context.Message.Split(' ');
                if (keys.Length >= 4 && keys[0].Equals("转发"))
                {
                    if (keys[1].Equals("群"))
                    {
                        string reStr=null;
                        for (int i = 3; i < keys.Length; i++)
                        {
                            reStr += keys[i];
                        }
                        _mahuaApi.SendGroupMessage(keys[2], reStr);
                    }
                }
            }
        }
    }
}
