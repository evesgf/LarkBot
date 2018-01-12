using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Jobs
{
    public class PrintJob : Job
    {
        public void Print()
        {
            Console.WriteLine("Test dynamic invoke...");
        }
    }
}
