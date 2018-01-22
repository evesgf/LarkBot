using LarkFramework.Module;
using UnityEngine;

namespace LarkFramework.Scenes.Example
{
    public class Scenes_Example : MonoBehaviour
    {
        void Start()
        {
            Init();
        }

        public void Init()
        {
            Debuger.EnableLog = true;

            ModuleManager.Instance.Init("LarkFramework.Module.Example");

            ScenesManager.Instance.Init("Scene/Example/");
            ScenesManager.MainScene = SceneExampleDef.Example;
            ScenesManager.Instance.LoadScene(SceneExampleDef.LoadOK);
        }
    }

    public static class SceneExampleDef
    {
        public const string Example = "Scenes_Example";
        public const string LoadOK = "LoadOK";
    }
}
