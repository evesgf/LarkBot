using Newbe.Mahua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.TimeJobs
{
    public interface ICoinNewsTimeJob
    {
        void StartPushNews();

        void CheckNews();

        void SendNews(string msg);
    }
}
