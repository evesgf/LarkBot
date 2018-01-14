using Core.Business;
using Core.Crawler;
using Core.Data;
using Core.TimeJob;
using Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core
{
    public static class QREntry
    {
        public static void UseQR(this IServiceCollection services)
        {
            Singleton<DataManager>.Create();
            Singleton<BusinessManager>.Create();

            Singleton<CrawlerManager>.Create();
            Singleton<TimeJobManager>.Create();

            Singleton<DataManager>.Instance.ServiceCollection(services);
            Singleton<BusinessManager>.Instance.ServiceCollection(services);

            //Singleton<CrawlerManager>.Instance.ServiceCollection(services);

            Singleton<TimeJobManager>.Instance.OpenTimeJob();
        }

        public static void UseQR(this IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {
            Singleton<DataManager>.Instance.ApplicationBuilder(app, env, context);
            Singleton<BusinessManager>.Instance.ApplicationBuilder(app, env, context);

            //Singleton<CrawlerManager>.Instance.ApplicationBuilder(app, env, context);
        }
    }
}
