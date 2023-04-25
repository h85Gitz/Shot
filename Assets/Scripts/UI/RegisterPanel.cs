using Manager;
using Models;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class RegisterPanel : MonoSingleton<RegisterPanel>
    {

        private InputField _account;
        private InputField _password;
        private InputField _confirmPassword;
        //private Toggle _isBoy;

        public Button backBtn;
        public Button inRegisterBtn;

        protected override void OnStart()
        {

        }

        public void Start()
        {
            _account = transform.Find("Account/InputField").GetComponent<InputField>();
            _password = transform.Find("Password/InputField").GetComponent<InputField>();
            _confirmPassword = transform.Find("RePassWord/InputField ").GetComponent<InputField>();
            //_isBoy = transform.Find("GenderGroup/BoyToggle").GetComponent<Toggle>();
            gameObject.SetActive(false);
        }

        public void BackBtnClick()
        {
            MainPanel.Instance.Show();
            //隐藏注册面板
            gameObject.SetActive(false);
        }

        public void InRegisterBtnClick()
        {
            //开始注册
            if (string.IsNullOrEmpty(_account.text)  ||
                string.IsNullOrEmpty(_password.text) ||
                string.IsNullOrEmpty(_confirmPassword.text))
            {
                PromptWindow.Instance.ChangeMessage("Some input fields are empty!");
            }
            else if (_password.text != _confirmPassword.text)
            {
                PromptWindow.Instance.ChangeMessage("Entered passwords differ!");
            }
            else
            {
                if (DateManager.Instance.GetInfo(_account.text) != null)
                {
                    PromptWindow.Instance.ChangeMessage("The account has been registered!");
                }
                else
                {
                    var infos = new ShowInfo(_account.text, _password.text/*, _isBoy.isOn*/);
                    DateManager.Instance.SaveInfo(infos);
                    PromptWindow.Instance.ChangeMessage("Registered successfully!");
                }
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _account.text = "";
            _password.text = "";
            _confirmPassword.text = "";
        }
    }
}
