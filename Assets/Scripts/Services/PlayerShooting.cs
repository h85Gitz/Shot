using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Services
{
    public class PlayerShooting : MonoSingleton<PlayerShooting>
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;

        private float _timer;
        private Ray _shootRay;
        private RaycastHit _shootHit;
        private int _shootTableMask;
        private ParticleSystem _gunParticles;
        private LineRenderer _gunLine;
        private AudioSource _gunAudio;
        private Light _gunLight;
        [FormerlySerializedAs("_effectDisplayTime")] [SerializeField]
        private float effectDisplayTime = 0.2f;
        private Transform _player;

        protected override void Awake()
        {

            _shootTableMask = LayerMask.GetMask("Bootable");
            _gunParticles = GetComponent<ParticleSystem>();
            _gunLine = GetComponent<LineRenderer>();
            _gunAudio = GetComponent<AudioSource>();
            _gunLight = GetComponent<Light>();
            _player = transform.GetComponentInParent<Transform>();

            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets)
            {
                ShootHit();

            }

            if (_timer >= timeBetweenBullets * effectDisplayTime)
            {
                DisableEffects();
            }

        }

        public void DisableEffects()
        {
            _gunLine.enabled = false;
            _gunLight.enabled = false;
        }

        private void ShootHit()
        {
            _timer = 0f;
            _gunAudio.Play();

            _gunLight.enabled = true;

            _gunParticles.Stop();
            _gunParticles.Play();

            _gunLine.enabled = true;
            _gunLine.SetPosition(0,transform.position);


            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;

            if (!Physics.Raycast(_shootRay, out _shootHit, range, _shootTableMask))
            {
                _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
                return;
            }
            var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamaged(damagePerShot, _shootHit.point);
            } _gunLine.SetPosition(1, _shootHit.point);
        }


    }
}
