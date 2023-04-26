using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Manager
{
    public class UIManager : MonoSingleton< UIManager>
    {
        [FormerlySerializedAs("_characterNameText")] [SerializeField ]
        private TextMeshProUGUI characterNameText;

        [FormerlySerializedAs("_dialogueLineText")] [SerializeField]
        private TextMeshProUGUI dialogueLineText;

        [FormerlySerializedAs("_dialogueBox")] [SerializeField]
        private GameObject dialogueBox;

        [FormerlySerializedAs("_spaceBar")] [SerializeField]
        private GameObject spaceBar;

        [SerializeField]
        private RectTransform arrowRectTransform; // ��ͷ UI Ԫ�ص� RectTransform
        [SerializeField]
        private Transform target; 
        [SerializeField]
        private Camera mainCamera; // ������������ڽ���������ת��Ϊ��Ļ����

        private void Update()
        {
            ArrowMove();
        }
        public void SetDialogueBoxToggle(bool isActive)
        {
            dialogueBox.SetActive(isActive);
        }

        public void SetSpaceBarToggle(bool isActive)
        {
            spaceBar.SetActive(isActive);
        }

        public void SetDialogue(string names, string  line)
        {
            characterNameText.text = names;
            dialogueLineText.text = line;
            dialogueBox.SetActive(true);
        }
        private void ArrowMove()
        {
            // ��Ŀ��λ�õ���������ת��Ϊ��Ļ����
            Vector2 screenPosition = mainCamera.WorldToScreenPoint(target.position);

            // �����ͷ UI Ԫ�ص����ĵ���Ŀ��λ��֮�����Ļ�����
            Vector2 direction = screenPosition - (Vector2)arrowRectTransform.position;

            // �����ͷָ��Ŀ��λ���������ת�Ƕ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ӧ����ת����ͷ UI Ԫ��
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, angle);

            // ��ʱ��
        }

    }
}