using Services;
using TMPro;
using UnityEngine.Serialization;
using Utilities;

namespace Manager
{
    //观察者类
    public class ScoreManager: MonoSingleton<ScoreManager>,EnemyHealth.IObserver
    {


        public  int score;

        private TextMeshProUGUI _text;


        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateValue(int value)
        {
            this.score += value;
            _text.text = "Score: " + score;
        }
    }
}