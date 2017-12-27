using Lark.Bot.CQA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = RequestHandler.GetBitPrice("eos_usdt");
            var a = RequestHandler.GetBitPrice2("btc");

            Console.ReadKey(true);
        }
    }
}
