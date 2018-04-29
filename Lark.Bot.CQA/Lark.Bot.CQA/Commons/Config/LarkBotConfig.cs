using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Commons.Config
{
    public class LarkBotConfig: ConfigBase
    {
        public string AdminQQ { get; set; }
        public string NewsServerURL { get; set; }
        public string HuobiAPI { get; set; }
        public string OkextAPI { get; set; }
    }
}
