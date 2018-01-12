using Core.Manager;
using Data;
using Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Data
{
    public class DataManager: Singleton<DataManager>,IManager
    {
        #region ServiceCollection
        public void ServiceCollection(IServiceCollection services)
        {
            RegisterEFService(services);
            RegisterRespositoryServices(services);
        }

        /// <summary>
        /// 注册EF服务
        /// </summary>
        /// <param name="services"></param>
        public void RegisterEFService(IServiceCollection services)
        {
            var mySqlConnection = "Data Source=localhost;port=3306;Initial Catalog=LarkBotServer;uid=root;password=123456;Charset=utf8;SslMode=None;";

            //增加EF服务
            services.AddDbContext<MySqlDbContext>(options => options.UseMySql(mySqlConnection));
        }

        /// <summary>
        /// 注册数据仓储
        /// 第三方Respository:https://www.cnblogs.com/xiaoliangge/p/7231715.html
        /// </summary>
        /// <param name="services"></param>
        public void RegisterRespositoryServices(IServiceCollection services)
        {
            services.AddUnitOfWork<MySqlDbContext>();
        }
        #endregion

        #region ApplicationBuilder
        public void ApplicationBuilder(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {
            InitializeDB(context);
        }

        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        public void InitializeDB(IServiceProvider context)
        {
            //var migrations = await context.Database.GetPendingMigrationsAsync();//获取未应用的Migrations，不必要，MigrateAsync方法会自动处理
            //根据migrations修改/创建数据库
            using (var ser = context.CreateScope())
            {
                ser.ServiceProvider.GetService<MySqlDbContext>().Database.Migrate();
            }
        }
        #endregion

        #region Mapping
        /// <summary>
        /// 遍历注册所有IDependencyRegister的service
        /// </summary>
        /// <param name="services"></param>
        public static void CreateMappings()
        {
            //services.AddTransient<ITestServices, TestServices>();

            //程序集
            //TODO:IOC程序集加载不指定名字
            //var assemblies = new Assembly[] { Assembly.GetEntryAssembly(), Assembly.Load("Business") };
            //var serviceType = typeof(IDependencyRegister);

            ////遍历子接口
            //var x = Assembly.GetEntryAssembly().FullName;
            //var a = assemblies.SelectMany(assembly => assembly.GetTypes());
            //var b = a.Where(type => serviceType.IsAssignableFrom(type) && type.GetTypeInfo().IsAbstract);
            //foreach (var service in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => serviceType.IsAssignableFrom(type) && type.GetTypeInfo().IsAbstract))
            //{
            //    if (service == serviceType) continue;

            //    //根据接口查找所有对应实例
            //    foreach (var implementationType in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => service.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract))
            //    {
            //        services.AddTransient(service, implementationType);
            //    }
            //}
        }
        #endregion
    }
}
