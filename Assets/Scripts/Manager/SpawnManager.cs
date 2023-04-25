using System;
using System.Threading;
using Controller;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Random = UnityEngine.Random;

namespace Manager
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [Serializable]
        public struct EnemyType
        {
            [FormerlySerializedAs("Enemy")] public GameObject[] enemy;

            [FormerlySerializedAs("SpawnPoints")] public Transform[] spawnPoints;
        }
        [FormerlySerializedAs("_enemyTypes")] [SerializeField]
        private EnemyType enemyTypes;
        public float maxTimeBetween = 3f;
        public float minTimeBetween = 1f;
        public float timeUntilMaxDifficult = 50f;
        public bool isGenerateEnemy;

        private float[] _range;
        private PlayerHealth _playerHealth;
        private float _elapsedTime = 0f;
        private float _spawnTime;

        protected override void Awake()
        {
            _spawnTime = maxTimeBetween;
            _range = new float[enemyTypes.enemy.Length];
            _playerHealth = PlayerHealth.Instance;
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_spawnTime <= 0 && isGenerateEnemy)
            {

                GenerateEnemy();
                _spawnTime = Mathf.Lerp(maxTimeBetween, minTimeBetween, _elapsedTime / timeUntilMaxDifficult);
            }
            else
            {
                _spawnTime -= Time.deltaTime;
            }
        }

        private void GenerateEnemy()
        {
            if (_playerHealth.CurrentHealth <= 0f) return;
            var enemyRandomRange = Random.Range(0, _range[^1]);
            var enemyIndex = _range.Length - 1;

            for (var i = 0; i < _range.Length; i++)
            {
                if (!(enemyRandomRange < _range[i])) continue;
                enemyIndex = i;
                break;
            }
            var spawnPointIndex = Random.Range(0, enemyTypes.spawnPoints.Length);
            var go = ObjectPool.Instance.GetPool(enemyTypes.enemy[enemyIndex]);
            go.transform.SetPositionAndRotation(enemyTypes.spawnPoints[spawnPointIndex].position,enemyTypes.spawnPoints[spawnPointIndex].rotation);
        }
    }
}