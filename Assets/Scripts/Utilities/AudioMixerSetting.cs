using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerSetting : MonoBehaviour
{
        [Header("音频混响器")]
        public AudioMixer mixer;

        public void SetBGMbolume(float Value)
        {
            mixer.SetFloat("BGM", Value);
        }
}
