using LarkFramework.Audio;
using LarkFramework.Module;
using LarkFramework.Utils;
using ProtoBuf;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LarkFramework.Config
{
    /// <summary>
    /// APP的配置定义
    /// </summary>
    [ProtoContract]
    public class AppConfig 
    {
        [ProtoMember(1)] public bool enableSoundEffect = true;              //音效
        [ProtoMember(2)] public bool enableMusic = true;                    //音乐
        [ProtoMember(3)] public bool enableBGM = true;                      //BGM

        //-------------------------------------------------------------------------

        private static AppConfig m_Value = new AppConfig();
        public static AppConfig Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// 保存配置事件
        /// </summary>
        public ModuleEvent onSave = new ModuleEvent();

#if UNITY_EDITOR
        public readonly static string Path = Application.persistentDataPath + "/AppConfig_Editor.data";
#else
        public readonly static string Path = Application.persistentDataPath + "/AppConfig.data";
#endif

        public static void Init()
        {
            if (!File.Exists(Path))
            {
                Save();
                Debuger.Log("AppConfig First Create", "Init() Path = " + Path);
            }

            byte[] data = FileUtils.ReadFile(Path);
            if (data != null && data.Length > 0)
            {
                AppConfig cfg = (AppConfig)PBSerializer.NDeserialize(data, typeof(AppConfig));
                if (cfg != null)
                {
                    m_Value = cfg;
                    Debuger.Log("AppConfig Load", "Init() Path = " + Path);
                }
            }
        }

        public static void Save()
        {
            Debuger.Log("AppConfig", "Save() Value = " + m_Value);

            if (m_Value != null)
            {
                byte[] data = PBSerializer.NSerialize(m_Value);
                FileUtils.SaveFile(Path, data);

                //通知AudioManager修改
                AudioManager.Instance.RefreshSetting(m_Value);
            }
        }
    }
}
