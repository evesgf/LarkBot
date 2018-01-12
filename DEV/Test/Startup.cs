using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestAPI;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var mySqlConnection = "Data Source=localhost;port=3306;Initial Catalog=EFCore2Test;uid=root;password=123456;Charset=utf8;SslMode=None;";

            //增加EF服务
            services.AddDbContext<MySqlDbContext1>(options => options.UseMySql(mySqlConnection));

            services.AddTimedJob()
                .AddEntityFrameworkDynamicTimedJob<MySqlDbContext1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTimedJob(); // 使用定时事务
            SampleData.InitDB(app.ApplicationServices); // 初始化数据库

            app.UseMvc();
        }
    }
}
