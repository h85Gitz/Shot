using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class PromptWindow : MonoSingleton<PromptWindow>
    {
        public Button confirmBtn ;
        private Text warningInfo;

        void Start()
        {

            warningInfo = transform.Find("WarningInfo").GetComponent<Text>();
            ConfirmBtnClick();
        }

        public void ChangeMessage(string message)
        {
            gameObject.SetActive(true);
            warningInfo.text = message;

        }

        public void ConfirmBtnClick()
        {
            gameObject.SetActive(false);
        }



    }
}
