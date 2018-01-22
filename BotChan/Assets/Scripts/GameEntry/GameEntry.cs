using System.Collections;
using System.Collections.Generic;
using LarkFramework;
using LarkFramework.Module;
using LarkFramework.ResourcesMgr;
using LarkFramework.Scenes;
using LarkFramework.Scenes.Example;
using LarkFramework.UI;
using UnityEngine;

namespace Project
{
    public class GameEntry : MonoBehaviour
    {

        public LaunchType lanuchType = LaunchType.Debug;
        public string startScene;
        public string startUI;

        public enum LaunchType
        {
            Debug = 1,
            Release = 2,
        }

        // Use this for initialization
        void Start()
        {
            Init();
        }

        public void Init()
        {
            switch (lanuchType)
            {
                case LaunchType.Debug:
                    DebugLaunch();
                    break;

                case LaunchType.Release:
                    ReleaseLaunch();
                    break;
            }

            ModuleManager.Instance.Init("Project");

            //InitModule

            SingletonMono<ResourcesMgr>.Create();

            ScenesManager.Instance.Init("Scene");
            //ScenesManager.MainScene = SceneExampleDef.Example;
            //ScenesManager.Instance.LoadScene(SceneExampleDef.LoadOK);

            UIManager.Instance.Init();
            //UIManager.MainPage = "UIPage1";
            //UIManager.Instance.EnterMainPage();

            DontDestroyOnLoad(gameObject);
        }

        private void DebugLaunch()
        {
            Debuger.EnableLog = true;

            if(!string.IsNullOrEmpty(startUI)) UIManager.Instance.OpenPage(startUI);
        }

        private void ReleaseLaunch()
        {
            Debuger.EnableLog = false;
        }
    }

}
