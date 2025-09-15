using UnityEngine;
using R3;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// スコアを管理するクラス
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private ReactiveProperty<int> _currentScore = new ReactiveProperty<int>(0);

        /// <summary>
        /// 現在のスコアのプロパティ
        /// </summary>
        public ReactiveProperty<int> CurrentScore => _currentScore;

        private const int ExtraBonusIntervalPoint = 1000000;
        private int _curentExtraBonusPoint;
        
        private MainGameManager _mainGameManager;

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public void Init()
        {
            _curentExtraBonusPoint = ExtraBonusIntervalPoint;
            _mainGameManager = MainGameManager.Instance;
            
            CurrentScore.Subscribe(score =>
            {
                if (score >= _curentExtraBonusPoint)
                {
                    // 正式な実装ではここでボーナスを付与する処理を追加
                    _curentExtraBonusPoint += ExtraBonusIntervalPoint;
                }
            });
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// スコアを追加するメソッド
        /// </summary>
        /// <param name="amount">増加量</param>
        public void AddScore(int amount)
        {
            _currentScore.Value += amount;
            Debug.Log($"Score added: {amount}, Total Score: {CurrentScore}");
        }

        /// <summary>
        /// スコアをリセットするメソッド
        /// </summary>
        public void ResetScore()
        {
            _currentScore.Value = 0;
            Debug.Log("Score reset to 0");
        }
    }
}
