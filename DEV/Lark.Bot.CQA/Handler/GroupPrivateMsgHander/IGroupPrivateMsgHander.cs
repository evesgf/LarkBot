﻿using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Handler.GroupPrivateMsgHander
{
    public interface IGroupPrivateMsgHander:IHandler
    {
        /// <summary>
        /// 传入关键词判断
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        HandlerResult CheckKeyWord(PrivateMessageFromGroupReceivedContext context);
    }
}
