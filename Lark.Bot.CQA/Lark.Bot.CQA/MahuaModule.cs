using Autofac;
using Autofac.Extras.Quartz;
using Lark.Bot.CQA.MahuaEvents;
using Lark.Bot.CQA.Services;
using Lark.Bot.CQA.Services.News;
using Lark.Bot.CQA.TimeJobs;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Quartz;

namespace Lark.Bot.CQA
{
    /// <summary>
    /// Ioc容器注册
    /// </summary>
    public class MahuaModule : IMahuaModule
    {
        public Module[] GetModules()
        {
            // 可以按照功能模块进行划分，此处可以改造为基于文件配置进行构造。实现模块化编程。
            return new Module[]
            {
                new PluginModule(),
                new MahuaEventsModule(),
            };
        }

        /// <summary>
        /// 基本模块
        /// </summary>
        private class PluginModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                // 将实现类与接口的关系注入到Autofac的Ioc容器中。如果此处缺少注册将无法启动插件。
                // 注意！！！PluginInfo是插件运行必须注册的，其他内容则不是必要的！！！
                builder.RegisterType<PluginInfo>()
                    .As<IPluginInfo>();

                //注册在“设置中心”中注册菜单，若想订阅菜单点击事件，可以查看教程。http://www.newbe.cf/docs/mahua/2017/12/24/Newbe-Mahua-Navigations.html
                builder.RegisterType<MyMenuProvider>()
                    .As<IMahuaMenuProvider>();
            }
        }

        /// <summary>
        /// <see cref="IMahuaEvent"/> 事件处理模块
        /// </summary>
        private class MahuaEventsModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                // 将需要监听的事件注册，若缺少此注册，则不会调用相关的实现类
                #region Init
                builder.RegisterType<InitEvent>().As<IInitializationMahuaEvent>();

                builder.RegisterType<ExceptionEvent>().As<IExceptionOccuredMahuaEvent>();

                //注册定时任务模块
                builder.RegisterModule(new QuartzAutofacFactoryModule());
                builder.RegisterModule(new QuartzAutofacJobsModule(typeof(GetNewsJob).Assembly));

                builder.RegisterType<CoinNewsTimeJob>().As<ICoinNewsTimeJob>().SingleInstance();
                #endregion

                #region Evennts
                builder.RegisterType<GMRMEvent>().As<IGroupMessageReceivedMahuaEvent>();
                builder.RegisterType<PMRMEvent>().As<IPrivateMessageReceivedMahuaEvent>();

                #endregion

                #region Services
                builder.RegisterType<MessageHanderService>().As<IMessageHanderService>();

                builder.RegisterType<CoinmarketcapService>().As<ICoinmarketcapService>();
                builder.RegisterType<OkexService>().As<IOkexService>();
                builder.RegisterType<HuobiService>().As<IHuobiService>();

                //新闻
                builder.RegisterType<NewsService>().As<INewsService>();

                builder.RegisterType<JinseService>().As<IJinseService>();
                builder.RegisterType<BishijieService>().As<IBishijieService>();
                builder.RegisterType<PmtownService>().As<IPmtownService>();
                #endregion
            }
        }
    }
}
