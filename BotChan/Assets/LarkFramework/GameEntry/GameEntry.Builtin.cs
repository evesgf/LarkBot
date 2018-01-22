using LarkFramework.Config;
using LarkFramework.Module;
using LarkFramework.Scenes;
using LarkFramework.UI;
using Project;
using LarkFramework.Console;
using LarkFramework.Tick;
using LarkFramework.Audio;
using LarkFramework.FSM;

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

            //InitConfig
            //AppConfig.Init();

            //InitModule
            //Init Resources
            SingletonMono<ResourcesMgr.ResourcesMgr>.Create();

            //Init Scene
            ScenesManager.Instance.Init("Scene/");
            ScenesManager.MainScene = SceneDef.HomeScene;
            if (lanuchType == LaunchType.Debug && !string.IsNullOrEmpty(startScene))
            {
                ScenesManager.Instance.LoadScene(startScene);
            }

            //Init UI
            UIManager.Instance.Init("UI/");
            UIManager.MainPage = UIDef.MainPage;
            if (lanuchType == LaunchType.Debug && !string.IsNullOrEmpty(startUI))
            {
                UIManager.Instance.OpenPage(startUI, null);
            }

            //Init Audio
            AudioManager.Instance.Init("Audio/");
            if (lanuchType == LaunchType.Debug && !string.IsNullOrEmpty(startAudio))
            {
                AudioManager.Instance.PlayBGM(AudioDef.BGM_MainBGM,0.2f);
            }
        }
    }
}
