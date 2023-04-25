using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

public class AudioManager : MonoSingleton<AudioManager>
{
    [Serializable]
    //组件属性
    public class Sound
    {
        [Header("音频剪辑")]
        public AudioClip clip;

        [Header("音频分组")]
        public AudioMixerGroup outputGroup;

        [Header("音频音量")]
        [Range(0, 1)]
        public float volume;

        [Header("音频是否自启动")]
        public bool playOnAwake;

        [Header("音频是否要循环播放")]
        public bool loop;
    }
    public List<Sound> sounds;
    public Dictionary<string, AudioSource> soundsDict = new Dictionary<string,AudioSource>();

    void Start()
    {
        foreach (var item in sounds)
        {
            GameObject go = new GameObject(item.clip.name);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            
            audioSource.transform.SetParent(transform);

            audioSource.loop = item.loop;
            audioSource.clip = item.clip;
            audioSource.volume = item.volume;
            audioSource.playOnAwake = item.playOnAwake;
            audioSource.outputAudioMixerGroup = item.outputGroup;

            //if (item.playOnAwake)
            //{
            //     audioSource.Play();
            //}
            soundsDict.Add(item.clip.name,audioSource);
        }
    }
    public void PlayAudio(string name, bool isWait)
    {
        if (!soundsDict.ContainsKey(name))
        {
            Debug.LogError("Audio" + name + "does not exist ");
            return;
        }
        if (isWait)
        {
            if (!soundsDict[name].isPlaying)
            {
                //如果是等待的情况 不在播放
                soundsDict[name].Play();
            }
        }
        else
        {
            soundsDict[name].Play();
        }
    }

    public void StopAudio(string name)
    {
        if (!soundsDict.ContainsKey(name))
        {
            Debug.LogError("Audio" + name + "does not exist ");
            return;
        }
        else
        {
            soundsDict[name].Stop();
        }
   
    }

  
}
