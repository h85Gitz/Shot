using UnityEngine;
using UnityEngine.Playables;

namespace Manager.Playables
{
    public class DialogueClip : PlayableAsset
    {
        public DialogueBehavior temple = new();
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var player = ScriptPlayable<DialogueBehavior>.Create(graph,temple);
            return player;
        }
    }
}