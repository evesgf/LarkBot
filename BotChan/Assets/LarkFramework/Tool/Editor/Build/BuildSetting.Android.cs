using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace LarkFramework.Tool
{
    public static partial class BuildSetting
    {
        /// <summary>
        /// 设置Android配置
        /// </summary>
        public static void SetAndroidSetting()
        {
            #region PlayerSettings
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
            #endregion
        }
    }
}
