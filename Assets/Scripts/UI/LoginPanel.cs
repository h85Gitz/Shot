using System.Collections;
using Controller;
using Manager;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class LoginPanel : MonoSingleton<LoginPanel>
    {
        private InputField _account;
        private InputField _password;
        private bool isMultPlay;

        public Button backBtn;
        public Button inRegisterBtn;


        public void Start()
        {
            _account = transform.Find("AccountLogin/InputField").GetComponent<InputField>();
            _password = transform.Find("PasswordLogin/InputField").GetComponent<InputField>();
            gameObject.SetActive(false);

            //注册事件
            Attach();
        }

        private void Attach()
        {
            NotifierDispense.Attach(CreateAndJoinRooms.Publisher.Name, OnMultPlay);
        }

        public void BackBtnClick()
        {
            //返回主面板
            MainPanel.Instance.Show();

            //隐藏注册面板
            gameObject.SetActive(false);
        }

        public void LoginBtnClick()
        {
            var loginInfo = DateManager.Instance.GetInfo(_account.text);
            if (string.IsNullOrEmpty(_account.text) ||
                string.IsNullOrEmpty(_password.text))
            {
                PromptWindow.Instance.ChangeMessage("Some input fields are empty!");
            }
            else if (loginInfo == null)
            {
                PromptWindow.Instance.ChangeMessage("User dose not exist!");
            }
            else if (loginInfo.Password != _password.text || loginInfo.Account != _account.text)
            {
                PromptWindow.Instance.ChangeMessage("Wrong password or account!");
            }
            else if (loginInfo.Password == _password.text)
            {
                PromptWindow.Instance.ChangeMessage("Login successfully!");
            }
        }


        public void Show()
        {
            gameObject.SetActive(true);
            _account.text = "";
            _password.text = "";
        }

        private void OnMultPlay(EventNotify eventNotify)
        {
            if (eventNotify is CreateAndJoinRooms.Publisher updateMessage)
            {
                isMultPlay = updateMessage.IsMultPlay;
                Debug.Log($"<color=yellow>isMultPlay:{isMultPlay}</color>");
            }
        }

        public void OnChangeScene()
        {
            if (isMultPlay)
            {
                StartCoroutine(ChangeScene(null));
            }
            else
            {
                PromptWindow.Instance.ChangeMessage("You would fight alone!");
                StartCoroutine(ChangeScene(0.5f));
            }
        }

        private static IEnumerator ChangeScene(float? time)
        {
            if (time.HasValue)
            {
                //需要将 time 类型转换成 float
                var value = time.Value;
                yield return new WaitForSeconds(value);
                SceneManager.LoadScene(1);
            }
        }
    }
}

