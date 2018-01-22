using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LarkFramework.Tool
{
    public class CloudBuild
    {
        static BuildPlayerOptions buildPlayerOptions;
        static string outputPath;

        [MenuItem("Lark Tools/Build/Windows")]
        public static void BuildWindows()
        {
            Build("Windows", OutPutPath(), SetVersion());
        }

        [MenuItem("Lark Tools/Build/Android")]
        public static void BuildAndrod()
        {
            Build("ios", OutPutPath(), SetVersion());
        }

        [MenuItem("Lark Tools/Build/IOS")]
        public static void BuildIOS()
        {
            Build("ios", OutPutPath(), SetVersion());
        }

        /// <summary>
        /// 命令行编译
        /// 注意参数名不要和系统预设参数名重复
        /// </summary>
        public static void CmdBuild()
        {
            Build(GetArg("-buildTarget1"), GetArg("-outputPath1"), GetArg("-version1"));
        }

        /// <summary>
        /// 从命令行打包时根据参数名获取参数
        /// 注意参数名不要和系统预设参数名重复
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }

        private static void Build(string buildTarget, string path, string version)
        {
            buildPlayerOptions = new BuildPlayerOptions();
            outputPath = path;

            BuildSetting.SetCommonSetting();

            switch (buildTarget)
            {
                case "windows":
                    TargetWindows();
                    break;

                case "android":
                    TargetAndroid();
                    break;

                case "ios":
                    TargetIOS();
                    break;

                default:
                    TargetWindows();
                    break;
            }

            //通用配置
            if (version != null && CheckVersion(version))
            {
                PlayerSettings.bundleVersion = version;
            }
            else
            {
                PlayerSettings.bundleVersion = SetVersion();
            }

            //获取Scene In Build列表里的场景
            var scenesList = new List<string>();
            foreach (var item in EditorBuildSettings.scenes)
            {
                //未激活的无视
                if (!item.enabled) continue;

                scenesList.Add(item.path);
                Debug.Log(item.path);
            }

            buildPlayerOptions.scenes = scenesList.ToArray();
            buildPlayerOptions.target = BuildTarget.StandaloneWindows;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        private static void TargetWindows()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);

            //TODO:相关配置
            BuildSetting.SetWindowsSetting();

            buildPlayerOptions.locationPathName = outputPath + "/" + PlayerSettings.productName + ".exe";
        }

        private static void TargetAndroid()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            //TODO:相关配置
            BuildSetting.SetAndroidSetting();

            buildPlayerOptions.locationPathName = outputPath + "/" + PlayerSettings.productName + ".apk";
        }

        private static void TargetIOS()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);

            //TODO:相关配置
            BuildSetting.SetIOSSetting();

            buildPlayerOptions.locationPathName = outputPath + "/" + PlayerSettings.productName + ".ipa";
        }

        /// <summary>
        /// 当前版本号+1
        /// </summary>
        /// <returns></returns>
        private static string SetVersion()
        {
            string[] s = PlayerSettings.bundleVersion.Split('.');

            int v1;
            int.TryParse(s[2], out v1);

            return s[0] + "." + s[1] + "." + (v1 + 1);
        }

        /// <summary>
        /// 返回打包路径
        /// </summary>
        /// <returns></returns>
        private static string OutPutPath()
        {
            string v1 = Application.dataPath.Remove(Application.dataPath.Length - 7, 7);
            return v1 + "/output";
        }

        /// <summary>
        /// 检查输入的版本号是小于当前版本号
        /// </summary>
        /// <returns></returns>
        private static bool CheckVersion(string newV)
        {
            string[] s1 = PlayerSettings.bundleVersion.Split('.');
            string[] s2 = newV.Split('.');

            int v1, v2, v3;
            int.TryParse(s1[0], out v1);
            int.TryParse(s1[1], out v2);
            int.TryParse(s1[2], out v3);

            int v11, v22, v33;
            int.TryParse(s2[0], out v11);
            int.TryParse(s2[1], out v22);
            int.TryParse(s2[2], out v33);

            if (v1 > v11) return false;
            if (v2 > v22) return false;
            if (v3 > v33) return false;

            return true;
        }
    }
}
