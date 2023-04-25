using System.Collections;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class MainPanel : MonoSingleton<MainPanel>
    {
        public Button registerBtn;
        public Button loginBtn;
        public GameObject TipPanel;
        public GameObject LoadingPanel;
        public GameObject JoinGameRoom;

        [SerializeField]
        private Slider slider;

        private Setting _setting ;

        protected override void Awake()
        {
            base.Awake();
            _setting = Setting.Instance;
        }

        private IEnumerator Start()
        {

            TipPanel.SetActive(true);
            LoadingPanel.SetActive(false);

            yield return new WaitForSeconds(2f);
            LoadingPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            TipPanel.SetActive(false);

            for (float i = 30; i < 100;)
            {
                i += (Random.Range(0.1f, 1.5f))/2;
                slider.value = Mathf.SmoothStep(0, 1, i);
                yield return new WaitForEndOfFrame();
            }
            LoadingPanel.SetActive(false);
            gameObject.SetActive(true);
            _setting.OnParticle(true);
            yield return null;
        }


        public void RegisterBtnClick()
        {
            RegisterPanel.Instance.Show();

            gameObject.SetActive(false);
        }

        public void LoginBtnClick()
        {
            LoginPanel.Instance.Show();

            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Shut()
        {
            gameObject.SetActive(false);
        }

        public void OnDual()
        {
            Invoke(nameof(Shut), 0.5f);
            JoinGameRoom.gameObject.SetActive(true);
            _setting.OnParticle(false);
        }

    }
}
