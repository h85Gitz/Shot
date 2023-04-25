using Controller;
using Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utilities;

namespace Zombie
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieMove : MonoSingleton<ZombieMove>
    {
        public  Slider slider;
        public bool isMove;
        private Transform _player;
        private NavMeshAgent _nav;
        private PlayerHealth _playerHealth;
        private EnemyHealth _enemyHealth;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_enemyHealth.currentHealth >0 && _playerHealth.CurrentHealth>0 && isMove)
            {
                _nav.enabled = true;
                _nav.SetDestination(_player.position);
            }
            else
            {
                _nav.enabled = false;
            }
        }

    }
}
