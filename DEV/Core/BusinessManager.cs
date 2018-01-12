using Business;
using Core.Manager;
using Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Business
{
    public class BusinessManager: Singleton<BusinessManager>, IManager
    {
        #region ServiceCollection
        public void ServiceCollection(IServiceCollection services)
        {
            RegisterBusinessIOCServices(services);
        }

        /// <summary>
        /// 遍历注册所有IDependencyRegister的service
        /// </summary>
        /// <param name="services"></param>
        public void RegisterBusinessIOCServices(IServiceCollection services)
        {
            //services.AddTransient<ITestServices, TestServices>();

            //程序集
            //TODO:IOC程序集加载不指定名字
            var assemblies = new Assembly[] { Assembly.GetEntryAssembly(), Assembly.Load("Business") };
            var serviceType = typeof(IDependencyRegister);

            //遍历子接口
            var x = Assembly.GetEntryAssembly().FullName;
            var a = assemblies.SelectMany(assembly => assembly.GetTypes());
            var b = a.Where(type => serviceType.IsAssignableFrom(type) && type.GetTypeInfo().IsAbstract);
            foreach (var service in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => serviceType.IsAssignableFrom(type) && type.GetTypeInfo().IsAbstract))
            {
                if (service == serviceType) continue;

                //根据接口查找所有对应实例
                foreach (var implementationType in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => service.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract))
                {
                    services.AddTransient(service, implementationType);
                }
            }
        }
        #endregion

        #region ApplicationBuilder
        public void ApplicationBuilder(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {

        }
        #endregion
    }
}
