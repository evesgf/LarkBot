using Microsoft.Extensions.DependencyInjection;
using Pomelo.AspNetCore.TimedJob;
using System;

namespace TestAPI
{
    public static class SampleData
    {
        public static void InitDB(IServiceProvider services)
        {
            var DB = services.GetRequiredService<MySqlDbContext1>();
            var TimedJobService = services.GetRequiredService<TimedJobService>();
            DB.Database.EnsureCreated();
            DB.TimedJobs.Add(new Pomelo.AspNetCore.TimedJob.EntityFramework.TimedJob
            {
                Id = "Test.Jobs.PrintJob.Print", // 按照完整类名+方法形式填写
                Begin = DateTime.Now,
                Interval = 3000,
                IsEnabled = true
            }); // 添加一个定时事务
            DB.SaveChanges();
            TimedJobService.RestartDynamicTimers(); // 增删改过数据库事务后需要重启动态定时器
        }
    }
}
