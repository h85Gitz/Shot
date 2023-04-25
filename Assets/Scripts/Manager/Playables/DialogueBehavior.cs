using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Manager.Playables
{
    [Serializable]
    public class DialogueBehavior : PlayableBehaviour
    {
        [FormerlySerializedAs("DialogueLine")] [TextArea(8, 1)] public string dialogueLine;
        [FormerlySerializedAs("CharacterName")] public string characterName;
        public bool requirePause;

        private PlayableDirector _playableDirector;
        private bool _isPauseScheduled;
        private bool _isClipPlayed;

        public override void OnPlayableCreate(Playable playable)
        {
            _playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (_isClipPlayed || !(info.weight > 0)) return;
            UIManager.Instance.SetDialogue(characterName, dialogueLine);
            if (!requirePause) return;
            _isPauseScheduled = true;
            _isClipPlayed = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            _isClipPlayed = false;

            if (_isPauseScheduled)
            {
                _isPauseScheduled = false;
                GameManager.Instance.PauseTimeLine(_playableDirector);
            }
            else
            {
                UIManager.Instance.SetDialogueBoxToggle(false);
            }
        }
    }
}