using UnityEngine;

namespace LarkFramework.GameEntry
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LarkFramework/GameEntry")]
    public partial class GameEntry : MonoBehaviour
    {
        public static LaunchType lanuchType = LaunchType.Debug;
        public static string startScene;
        public static string startUI;
        public static string startAudio;

        public enum LaunchType
        {
            Debug = 1,
            Release = 2,
        }

        // Use this for initialization
	    void Awake()
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

            InitBuiltinComponents();
            InitCustomComponents();

            DontDestroyOnLoad(gameObject);
        }

        private void DebugLaunch()
        {
            Debuger.EnableLog = true;
        }

        private void ReleaseLaunch()
        {
            Debuger.EnableLog = false;
        }
    }

}
