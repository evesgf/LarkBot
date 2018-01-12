using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Jobs
{
    public class TestJob: Job
    {
        //[Invoke(Begin = "2018-1-9 00:00", Interval = 5000, SkipWhileExecuting = true)]
        //public void Run()
        //{
        //    Console.WriteLine("11111111111");
        //}
    }
}
