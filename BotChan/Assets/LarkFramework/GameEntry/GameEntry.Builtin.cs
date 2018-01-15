using LarkFramework.FSM;
using LarkFramework.Module;
using LarkFramework.Tick;

namespace LarkFramework.GameEntry
{
    public partial class GameEntry
    {
        public static void InitBuiltinComponents()
        {
            //Init Module
            ModuleManager.Instance.Init("Project");

            //InitTick
            TickManager.Instance.Init();

            //Init Console
            Console.Console.Create();

            //Init FSM
            FSMManager.Instance.Init();
        }
    }
}
