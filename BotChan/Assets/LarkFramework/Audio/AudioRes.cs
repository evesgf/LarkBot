using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Audio
{
    public static class AudioRes
    {
        public static string AudioResRoot = "Audio/";

        /// <summary>
        /// 加载要播放的Clip
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AudioClip LoadClip(string name)
        {
            Debug.Log("Load Clip:" + AudioResRoot + name);
            AudioClip asset = (AudioClip)Resources.Load(AudioResRoot + name);
            return asset;
        }
    }
}
