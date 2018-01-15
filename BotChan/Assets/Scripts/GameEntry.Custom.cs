using BestHTTP;
using BotChan;
using LarkFramework.Module;

namespace LarkFramework.GameEntry
{
    public partial class GameEntry
    {
        public static void InitCustomComponents()
        {
            ModuleManager.Instance.Init("BotChan");

            //实例化币圈消息模块
            Singleton<CoinModule>.Create().Init();

        }
    }
}
