using System;
using Manager;
using Photon.Pun;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviourPun
    {
        public bool isLookAtMouse;

        private GameManager _gameManager;
        void Start()
        {
            // animator = GetComponent<Animator>();
            // _rb = GetComponent<Rigidbody>();
            _gameManager = GameManager.Instance;

        }
        private void Update()
        {
            if ( _gameManager.gameMode != GameManager.GameMode.Normal) return;
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            PlayerMove.Instance.Animating(horizontal, vertical);
            PlayerMove.Instance.Move(horizontal, vertical);
            if (isLookAtMouse)
            {
                PlayerMove.Instance.LookAtMouse();
            }
        }
    }
}