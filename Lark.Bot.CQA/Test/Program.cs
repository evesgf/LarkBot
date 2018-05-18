using Lark.Bot.CQA.Services.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            var a = new BishijieService();

            var str = a.GetLatestNewsFlash().Result;
            Console.WriteLine(str);

            Console.ReadKey(true);
        }
    }
}
