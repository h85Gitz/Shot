using System.Collections.Generic;
using Manager;
using Models;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utilities;

namespace Services
{

    public class EnemyHealth : MonoSingleton<EnemyHealth>
    {
        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;
        private readonly List<IObserver> _observers = new List<IObserver>();
        [FormerlySerializedAs("_hitParticles")] public GameObject hitParticles;

        private ScoreManager _scoreManager;
        private Animator _anim;
        private AudioSource _enemyAudio;
        private CapsuleCollider _capsuleCollider;
        private bool _isDead;
        private int _score;

        private static readonly int Dead = Animator.StringToHash("Dead ");
        private bool IsSinking { get;  set; }


        public interface IObserver
        {
            public void UpdateValue(int value);
        }

        private void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        private void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateValue(_score);
            }
        }


        protected override void Awake()
        {
            _anim = GetComponent<Animator>();
            _enemyAudio = GetComponent<AudioSource>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _scoreManager = ScoreManager.Instance;

            currentHealth = startingHealth;
        }
        private void Start()
        {
            if (IsSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamaged(int amount,Vector3 pos)
        {
            if (_isDead) return;

            currentHealth -= amount;
            _enemyAudio.Play();

            var go = ObjectPool.Instance.GetPool(hitParticles);
            go.transform.position = pos;
            Invoke(nameof(DestroyEffect), 0.5f);

            if (currentHealth <= 0)
            {
                Death();
            }

        }

        private void Death()
        {
            _isDead = true;
            _capsuleCollider.isTrigger = true;

            _anim.SetTrigger(Dead);
            _enemyAudio.clip = AudioManager.Instance.soundsDict["ZomBunny Death"].clip;
            _enemyAudio.Play();
        }

        public void StartSinking()
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            IsSinking = true;
            Attach(_scoreManager);
            _score += scoreValue;

            Notify();

            Destroy(gameObject,2f);
        }

        private void DestroyEffect()
        {
            ReturnEffect(hitParticles);
        }

         private static void ReturnEffect(GameObject go)
        {
            ObjectPool.Instance.PushPool(go);
        }

    }

}