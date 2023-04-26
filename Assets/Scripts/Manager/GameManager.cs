using System.Collections.Generic;
using Cinemachine;
using Controller;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Utilities;
using Zombie;

namespace Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public enum GameMode
        {
            Normal,
            GamePlay,
            DialogueMoment
        }
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private Transform icon;
        [SerializeField] private Transform tip;
        [SerializeField] private Transform director;
        [SerializeField] private GameObject scoreBoard;
        public  GameMode gameMode;

        public Camera mainCamera;
        public Transform triggerArea;
        private PlayableDirector _currentPlayableDirector;
        private EffectType _effectType;
        private  PlayerController _playerController ;
        private RectTransform _rectIcon;
        private RectTransform _rectTip;
        private PlayableDirector _director;
        // private bool _isLookAtMouse;
        private Dictionary<int,int> _dictionary;
        protected override void Awake()
        {
            gameMode = GameMode.Normal;
            _playerController = FindObjectOfType<PlayerController>();
            _director = director.GetComponent<PlayableDirector>();
            // _isLookAtMouse = _playerController.isLookAtMouse;


        }
        private void Start()
        {
            _rectIcon = icon.GetComponent<RectTransform>();
            _rectTip = tip.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && gameMode == GameMode.DialogueMoment)
            {
                 ResumeTimeLine();
            }
        }

        public void PauseTimeLine(PlayableDirector playableDirector)
        {
            _currentPlayableDirector = playableDirector;
            gameMode = GameMode.DialogueMoment;
            _currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
            UIManager.Instance.SetSpaceBarToggle(true);
        }

        private void ResumeTimeLine()
        {
            gameMode = GameMode.GamePlay;
            _currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
            UIManager.Instance.SetSpaceBarToggle(false);
            UIManager.Instance.SetDialogueBoxToggle(true);
        }

        public  void StartGame()
        {
            _effectType = EffectType.Icon;
            gameMode = GameMode.Normal;
            SetStateActive(true);
            mainCamera.transform.GetComponent<CinemachineBrain>().enabled = false;
            UIEffect.Instance.Move(_effectType, _rectIcon, _rectTip,hpSlider, mpSlider);
           
        }

        private void SetStateActive(bool startGame)
        {
            if (startGame)
            {
                scoreBoard.SetActive(true);
                ZombieMove.Instance.isMove = true;
                _playerController.isLookAtMouse = true;
                SpawnManager.Instance.isGenerateEnemy = true;

                _director.gameObject.SetActive(false);
                UIManager.Instance.SetDialogueBoxToggle(false);
                triggerArea.transform.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
