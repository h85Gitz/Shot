using Controller;
using UnityEngine;

namespace Services
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage ;

        private PlayerHealth _playerHealth ;
        private Animator _anim;

        private bool _playerInRange;
        private float _timer;

        private int _enemyHealth ;
        private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");

        private void Start()
        {
            _enemyHealth = EnemyHealth.Instance.currentHealth;
            _playerHealth = PlayerHealth.Instance;
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _playerInRange = other.CompareTag("Player") ;

        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
            }
            //Debug.Log("OnTriggerExit:" + _playerInRange);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= timeBetweenAttacks && _playerInRange && _enemyHealth>0)
            {
                Attack();
            }
            if (_playerHealth.CurrentHealth <= 0)
            {
                _anim.SetTrigger(PlayerDead);
            }
        }

        private void Attack()
        {
            _timer = 0;
            if(_playerHealth.CurrentHealth > 0)
            {
                PlayerHealth.Instance.TakeDamaged(attackDamage);
            }
        }
    }
}
