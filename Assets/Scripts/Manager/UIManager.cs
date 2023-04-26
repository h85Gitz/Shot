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
        private RectTransform arrowRectTransform; // 箭头 UI 元素的 RectTransform
        [SerializeField]
        private Transform target; 
        [SerializeField]
        private Camera mainCamera; // 主摄像机，用于将世界坐标转换为屏幕坐标

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
            // 将目标位置的世界坐标转换为屏幕坐标
            Vector2 screenPosition = mainCamera.WorldToScreenPoint(target.position);

            // 计算箭头 UI 元素的中心点与目标位置之间的屏幕坐标差
            Vector2 direction = screenPosition - (Vector2)arrowRectTransform.position;

            // 计算箭头指向目标位置所需的旋转角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 应用旋转到箭头 UI 元素
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, angle);

            // 计时器
        }

    }
}