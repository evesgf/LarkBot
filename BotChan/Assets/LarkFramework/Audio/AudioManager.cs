using LarkFramework.Module;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Audio
{
    public class AudioManager : ServiceModule<AudioManager>
    {
        public const string LOG_TAG = "AudioManager";

        public AudioComponent audioComponent;

        public void Init(string audioResRoot = "Audio/")
        {
            CheckSingleton();

            AudioRes.AudioResRoot = audioResRoot;
            audioComponent = AudioComponent.Find();

            audioComponent.dic_Music = new Dictionary<string, AudioSource>();
            audioComponent.dic_SoundEffect = new Dictionary<string, AudioSource>();
        }

        #region PlayBGM
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名</param>
        /// <param name="volume">音量</param>
        /// <param name="isLoop">是否循环，默认循环</param>
        public void PlayBGM(string name, float volume=1, bool isLoop = true)
        {
            AudioClip clip = AudioRes.LoadClip(name);
            audioComponent.PlayBGM(name, clip, volume, isLoop);
        }
        #endregion

        #region PlayMusic
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">音乐名</param>
        /// <param name="volume">音量</param>
        /// <param name="isLoop">是否循环，默认不循环</param>
        public void PlayMusic(string name, float volume = 1, bool isLoop = false)
        {
            AudioClip clip = AudioRes.LoadClip(name);
            audioComponent.PlayMusic(name, clip, volume, isLoop);
        }
        #endregion

        #region PlaySoundEffect
        /// <summary>
        ///播放特效音
        /// </summary>
        /// <param name="name">音乐名</param>
        /// <param name="volume">音量</param>
        /// <param name="isLoop">是否循环，默认不循环</param>
        public void PlaySoundEffect(string name, float volume = 1, bool isLoop = false)
        {
            AudioClip clip = AudioRes.LoadClip(name);
            audioComponent.PlaySoundEffect(name, clip, volume, isLoop);
        }
        #endregion

        #region Mute
        /// <summary>
        /// 开关BGM
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableBGM(bool isEnable = true)
        {
            audioComponent.EnableBGM(isEnable);
        }

        /// <summary>
        /// 开关Music
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableMusic(bool isEnable = true)
        {
            audioComponent.EnableMusic(isEnable);
        }

        /// <summary>
        /// 开关SoundEffect
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableSoundEffect(bool isEnable = true)
        {
            audioComponent.EnableSoundEffect(isEnable);
        }
        #endregion
    }
}
