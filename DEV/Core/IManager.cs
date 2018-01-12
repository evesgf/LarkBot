using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Manager
{
    public interface IManager
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        void ServiceCollection(IServiceCollection services);

        /// <summary>
        /// his method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        void ApplicationBuilder(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context);
    }
}
