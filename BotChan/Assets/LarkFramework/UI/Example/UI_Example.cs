using LarkFramework.Module;
using UnityEngine;

namespace LarkFramework.UI.Example
{
    public class UI_Example : MonoBehaviour
    {
        void Start()
        {
            Init();
        }

        public void Init()
        {
            Debuger.EnableLog = true;

            ModuleManager.Instance.Init("LarkFramework.Module.Example");

            UIManager.Instance.Init("UI/Example/");
            UIManager.MainPage = "UIPage1";
            UIManager.Instance.EnterMainPage();
        }
    }

    public static class UIExampleDef
    {
        public const string MainUI = "";
    }
}
