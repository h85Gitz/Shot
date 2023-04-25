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

    }
}