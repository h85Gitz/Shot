using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Services
{
    public  class Setting : MonoSingleton<Setting>
    {
        [SerializeField] private ParticleSystem firstpartical;

        [FormerlySerializedAs("particalsecond")] [SerializeField]
        private ParticleSystem secondpartical;

        public void OnVolumeChange(float value)
        {
            AudioManager.Instance.sounds[1].volume = value;
        }

        public void OnLoop(bool value)
        {
            AudioManager.Instance.sounds[1].loop = value;
        }

        public void OnPlayOnAwake(bool value)
        {
            AudioManager.Instance.sounds[1].playOnAwake = value;
        }

        public void OnParticle(bool isPlay)
        {
            if (isPlay)
            {
                firstpartical.Play();
                secondpartical.Play();
            }
            else
            {
                firstpartical.Stop();
                secondpartical.Stop();
            }
        }
    }
}
