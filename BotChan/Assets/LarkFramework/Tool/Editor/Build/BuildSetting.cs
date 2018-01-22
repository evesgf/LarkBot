using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LarkFramework.Tool
{
    public static partial class BuildSetting
    {
        /// <summary>
        /// 项目初始化配置
        /// </summary>
        [MenuItem("Lark Tools/Project Init Settings")]
        public static void ProjectInitSettings()
        {
            SetCommonSetting();
            SetWindowsSetting();
            SetAndroidSetting();
            SetIOSSetting();

            Debug.Log("Project Init Settings Finish!");
        }

        /// <summary>
        /// 打包时的配置项
        /// </summary>
        public static void SetBuildSetting()
        {
            SetCommonSetting();
            SetWindowsSetting();
            SetAndroidSetting();
            SetIOSSetting();

            Debug.Log("Project Init Settings Finish!");
        }

        /// <summary>
        /// 设置公共配置
        /// </summary>
        public static void SetCommonSetting()
        {
            #region PlayerSettings
            PlayerSettings.companyName = "Xile91.com";
            if (string.IsNullOrEmpty(PlayerSettings.productName))
                PlayerSettings.productName = SetProductName(Application.productName);
            PlayerSettings.defaultIsFullScreen = true;
            PlayerSettings.defaultScreenWidth = 1920;
            PlayerSettings.defaultScreenHeight = 1080;
            PlayerSettings.defaultIsNativeResolution = true;
            PlayerSettings.runInBackground = false;
            PlayerSettings.captureSingleScreen = false;
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;

            PlayerSettings.forceSingleInstance = true;
#if UNITY_5_6
            PlayerSettings.SplashScreen.show = false;
#endif
            PlayerSettings.applicationIdentifier = "com.xile91." + SetProductName(Application.productName);
            //PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.apiCompatibilityLevel = ApiCompatibilityLevel.NET_2_0;
            #endregion

            #region EditorSettings
            if (EditorSettings.externalVersionControl != "Visible Meta Files")
                EditorSettings.externalVersionControl = "Visible Meta Files";

            if (EditorSettings.serializationMode != SerializationMode.ForceText)
                EditorSettings.serializationMode = SerializationMode.ForceText;

            if (EditorSettings.unityRemoteDevice.Equals("None"))
                EditorSettings.unityRemoteDevice = "Any Android Device";
            #endregion

        }

        private static string SetProductName(string productName)
        {
            string name = "";
            name = productName.Replace("JT-", "");
            name = productName.Replace("RT-", "");
            name = productName.Replace("KJ-", "");
            name = productName.Replace("GC-", "");

            return name;
        }
    }
}
