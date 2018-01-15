using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Audio
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LarkFramework/AudioComponent")]
    public class AudioComponent : MonoBehaviour
    {
        public GameObject BGMPrefab;
        public GameObject MusicPrefab;
        public GameObject SoundEffectPrefab;

        public Transform Root_BGM;
        public Transform Root_Music;
        public Transform Root_SoundEffect;

        public AudioSource audio_BGM;
        public Dictionary<string, AudioSource> dic_Music;
        public Dictionary<string, AudioSource> dic_SoundEffect;

        public bool isEnableBGM;
        public bool isEnableMusic;
        public bool isEnableSoundEffect;

        public static AudioComponent Find()
        {
            return FindObjectOfType<AudioComponent>();
        }

        #region PlayAduio
        /// <summary>
        /// 播放BGM
        /// TODO:缺缓存释放机制
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="isLoop"></param>
        public void PlayBGM(string name, AudioClip clip, float volume = 1, bool isLoop = false)
        {
            if (audio_BGM == null)
            {
                audio_BGM = Instantiate(BGMPrefab, Root_BGM).GetComponent<AudioSource>();
                audio_BGM.name = clip.name;
            }

            audio_BGM.clip = clip;
            audio_BGM.volume = volume;
            audio_BGM.loop = isLoop;

            audio_BGM.mute = !isEnableBGM;

            audio_BGM.Play();
        }

        /// <summary>
        /// 播放Music
        /// TODO:缺缓存释放机制
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="isLoop"></param>
        public void PlayMusic(string name, AudioClip clip, float volume = 1, bool isLoop = false)
        {
            AudioSource audio = null;

            if (dic_Music.ContainsKey(name))
            {
                audio_BGM = dic_Music[name];
            }
            else
            {
                var obj = Instantiate(MusicPrefab, Root_Music).GetComponent<AudioSource>();
                obj.name = name;
                dic_Music.Add(name, obj);
            }

            audio.clip = clip;
            audio.volume = volume;
            audio.loop = isLoop;

            audio.mute = !isEnableMusic;

            audio.Play();
        }

        /// <summary>
        /// 播放SoundEffect
        /// TODO:缺缓存释放机制
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="isLoop"></param>
        public void PlaySoundEffect(string name, AudioClip clip, float volume = 1, bool isLoop = false)
        {
            AudioSource audio = null;

            if (dic_SoundEffect.ContainsKey(name))
            {
                audio_BGM = dic_Music[name];
            }
            else
            {
                var obj = Instantiate(SoundEffectPrefab, Root_SoundEffect).GetComponent<AudioSource>();
                obj.name = name;
                dic_SoundEffect.Add(name, obj);
            }

            audio.clip = clip;
            audio.volume = volume;
            audio.loop = isLoop;

            audio.mute = !isEnableMusic;

            audio.Play();
        }
        #endregion

        #region Mute
        /// <summary>
        /// 开关BGM
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableBGM(bool isEnable = true)
        {
            isEnableBGM = isEnable;

            if (audio_BGM != null)
            {
                audio_BGM.mute = !isEnableBGM;
            }
        }

        /// <summary>
        /// 开关Music
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableMusic(bool isEnable = true)
        {
            isEnableMusic = isEnable;

            foreach (var item in dic_Music)
            {
                item.Value.mute = !isEnableMusic;
            }
        }

        /// <summary>
        /// 开关SoundEffect
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableSoundEffect(bool isEnable = true)
        {
            isEnableSoundEffect = isEnable;

            foreach (var item in dic_SoundEffect)
            {
                item.Value.mute = !isEnableSoundEffect;
            }
        }
        #endregion
    }
}
