using Microsoft.EntityFrameworkCore;
using Pomelo.AspNetCore.TimedJob.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI
{
    public class MySqlDbContext1 : DbContext, ITimedJobContext
    {
        public MySqlDbContext1(DbContextOptions opt) 
            : base(opt)
        {
        }

        public DbSet<TimedJob> TimedJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SetupTimedJobs();
        }
    }
}
