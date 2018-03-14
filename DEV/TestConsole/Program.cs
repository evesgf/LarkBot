using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var a=HttpUitls.Get("https://www.okex.com/api/v1/ticker.do?symbol=btc_usdt");

            Console.ReadKey(true);
        }
    }
}
