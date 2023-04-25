using Manager;
using UnityEngine;
using UnityEngine.Playables;

namespace Controller
{
    public class TriggerArea : MonoBehaviour
    {
        public PlayableDirector playableDirector;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            playableDirector.Play();
            GameManager.Instance.gameMode = GameManager.GameMode.GamePlay;
        }
    }
}