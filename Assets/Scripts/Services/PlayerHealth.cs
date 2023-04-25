using Services;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controller
{
     class PlayerHealth : MonoSingleton<PlayerHealth>
    {
        public int startingHealth = 100;
        public  int CurrentHealth;
        public Slider healthSlider;
        public Image damageImage;

        public GameObject[] bloods;

        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f,0f,0f,0.1f);

        private Animator _anim;
        private AudioSource _playerAudio;
        private PlayerMove _playerMove;
        private PlayerShooting _playerShooting;

        readonly float[] nums =new float[3];
        private int _bloodFlags = 3;
        bool isDead;
        bool damaged;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _playerAudio = GetComponent<AudioSource>();
            _playerMove = GetComponent<PlayerMove>();
            _playerShooting = PlayerShooting.Instance;
            CurrentHealth = startingHealth;
        }

        private void Update()
        {
            damageImage.color = damaged ? flashColour : Color.Lerp(damageImage.color,Color.clear,flashSpeed * Time.deltaTime);
            damaged = false;
        }

        public void TakeDamaged(int amount)
        {
            damaged = true;
            CurrentHealth -= amount;
            healthSlider.value = (float)CurrentHealth/100;

            SetBloods(CurrentHealth, startingHealth);
            _playerAudio.Play();

            if (CurrentHealth <= 0 && !isDead)
            {
                Dead();
            }
        }

        private void Dead()
        {
            isDead = true;
            _playerShooting.DisableEffects();

            _anim.SetTrigger("IsDead");
            _playerAudio.clip = ChangeClip("Player Death");

            _playerAudio.Play();
            _playerMove.enabled = false;
            _playerShooting.enabled = false;
        }

        private static AudioClip ChangeClip(string name)
        {
            var clip = AudioManager.Instance.soundsDict[name].clip;
            return clip;
        }

        private void SetBloods(int value, int fullHp)
        {

            SetNums(fullHp);
            for (var i = 0; i < bloods.Length; i++)
            {

                if (value == nums[i])//Mathf.Approximately(value, nums[i])
                {
                    bloods[i].SetActive(false);
                    Debug.Log("bloods",bloods[i]);
                    _bloodFlags--;
                }
            }

        }

        private void SetNums(int fullHp)
        {
          for (int j = 2; j >= 0; j--)
          {
             int a = fullHp * (j + 1) / 4;
             nums[2-j]=a;

          }
        }
    }
 }
