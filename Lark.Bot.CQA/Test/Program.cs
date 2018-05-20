using Lark.Bot.CQA.Services.News;
using Lark.Bot.CQA.Uitls;
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

            //var str1= "币世界】【GitHub 90天提交排名：EOS为第一，其次是TRX】据CryptoMiso数据显示，在过去三个月GitHub提交代码更新第一为EOS，其次为TRX。具体如下：EOS（1927）、TRX（1834）、NULS（1611）、ZSC（1032）、RHOC（1006）、MOT（968）、AION（914）、ZIL（887）、LSK（869）和TRAC（858）。在统计的327种加密货币中，BTC排名25（提交423次）；BCH排名71（提交129次）；ETH排名61（提交161次）；ETC排名52（提交188次）；XRP排名116（提交43次）；LTC排名274（提交2次）。";
            //var str2 = "【金色财经】【GitHub 90天提交排名：EOS为第一，其次是TRX】据CryptoMiso数据显示，在过去三个月GitHub提交代码更新第一为EOS，其次为TRX。具体如下：EOS（1927）、TRX（1834）、NULS（1611）、ZSC（1032）、RHOC（1006）、MOT（968）、AION（914）、ZIL（887）、LSK（869）和TRAC（858）。 在统计的327种加密货币中，BTC排名25（提交423次）；BCH排名71（提交129次）；ETH排名61（提交161次）；ETC排名52（提交188次）；XRP排名116（提交43次）；LTC排名274（提交2次）。";

            //// 方式一
            //StringCompute stringcompute1 = new StringCompute();
            //stringcompute1.Compute(str1, str2);    // 计算相似度， 不记录比较时间
            //// 相似度百分之几，完全匹配相似度为1
            //Console.WriteLine("相似度："+ stringcompute1.ComputeResult.Rate + " 耗时:"+ stringcompute1.ComputeResult.UseTime+" 差异:"+ stringcompute1.ComputeResult.Difference);

            Console.ReadKey(true);
        }
    }
}
