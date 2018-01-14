using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.TrackCoin
{
    public class TrackInfoModel
    {
        public string coin { get; set; }
        public decimal price { get; set; }
        public bool isUp { get; set; }
        public string fromQQ { get; set; }
        public string fromGroup { get; set; }
    }
}
