using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.TrackCoin
{
    public class OKTrickModel
    {
        public string date { get; set; }
        public ticker ticker { get; set; }
    }

    public class ticker
    {
        public decimal buy { get; set; }
        public decimal high { get; set; }
        public decimal last { get; set; }
        public decimal low { get; set; }
        public decimal sell { get; set; }
        public double vol { get; set; }
    }
}
