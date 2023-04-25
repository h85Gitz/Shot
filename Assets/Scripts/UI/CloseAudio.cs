using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CloseAudio : MonoBehaviour
    {
        private Toggle _toggle;
        private AudioSource _bgAudio;
        void Start()
        {
            _toggle = GetComponent<Toggle>();
            _bgAudio = transform.Find("BgAudio").GetComponent<AudioSource>();
            _toggle.onValueChanged.AddListener(OnChangeAudio);
        }

        private void OnChangeAudio(bool isOn)
        {
            if (_toggle.isOn)
            {
                _bgAudio.Play();
            }
            else
            {
                _bgAudio.Stop();
            }
        }


    }
}
