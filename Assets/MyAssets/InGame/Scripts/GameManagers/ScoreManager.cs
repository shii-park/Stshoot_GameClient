using UnityEngine;
using R3;

namespace StShoot.InGame.GameManagers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private ReactiveProperty<int> _score = new ReactiveProperty<int>(0);

        public ReactiveProperty<int> Score => _score;

        private const int ExtraBonusIntervalPoint = 1000000;
        private int _curentExtraBonusPoint;

        private void Awake()
        {
            _curentExtraBonusPoint = ExtraBonusIntervalPoint;
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Score.Subscribe(score =>
            {
                if (score >= _curentExtraBonusPoint)
                {
                    // 正式な実装ではここでボーナスを付与する処理を追加
                    _curentExtraBonusPoint += ExtraBonusIntervalPoint;
                }
            });
        }

        public void AddScore(int amount)
        {
            _score.Value += amount;
            Debug.Log($"Score added: {amount}, Total Score: {Score}");
        }

        public void ResetScore()
        {
            _score.Value = 0;
            Debug.Log("Score reset to 0");
        }
    }
}
