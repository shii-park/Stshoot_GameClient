using System.Collections;
using R3;
using StShoot.InGame.Enemies.Bullets;
using StShoot.InGame.Items;
using StShoot.InGame.Players;
using UnityEngine;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// ゲーム全体の進行を管理するクラス
    /// </summary>
    public class MainGameManager : MonoBehaviour
    {
        public static MainGameManager Instance { get; private set; }
        
        private GameStateReactiveProperty _currentState = new GameStateReactiveProperty(GameState.Init);

        /// <summary>
        /// 現在のゲームの状態
        /// </summary>
        public ReadOnlyReactiveProperty<GameState> CurrentGameState => _currentState;
        
        [SerializeField]
        private TimeManager _timeManager;
        
        [SerializeField]
        private ScoreManager _scoreManager;
        
        [SerializeField]
        private GameProgressManager _gameProgressManager;
        
        [SerializeField]
        private PlayerCore _playerCore;
        
        public Transform PlayerPosition => _playerCore.gameObject.transform;
        
        [SerializeField]
        private Transform _playerDefaultTransform;
        
        private int _currentStageIndex = 0;
        /// <summary>
        /// 現在のステージのインデックス
        /// </summary>
        public int CurrentStageIndex => _currentStageIndex;
        
        void Awake()
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
            
            _currentStageIndex = 0;
            _currentState.Subscribe(state =>
            {
                OnStateChanged(state);
            });
            
            _playerCore.IsDead
                .Where(isDead => isDead)
                .Subscribe(_ =>
                {
                    EnemyBulletFactory.Instance?.StopBulletGeneration(1.0f);
                    EnemyBulletFactory.Instance?.RemoveAllBullets();
                });
            
            _playerCore.IsGameOver
                .Where(isGameOver => isGameOver)
                .Subscribe(_ =>
                {
                    SetGameState(GameState.Result);
                });
        }
        
        /// <summary>
        /// ゲームの状態を設定するメソッド
        /// </summary>
        /// <param name="nextState">次の状態</param>
        public void SetGameState(GameState nextState)
        {
            _currentState.Value = nextState;
        }
        
        /// <summary>
        /// ゲームの状態が変化したときに呼ばれるメソッド
        /// </summary>
        /// <param name="nextState">次の状態</param>
        void OnStateChanged(GameState nextState)
        {
            switch (nextState)
            {
                case GameState.Init:
                    StartCoroutine(InitCoroutine());
                    break;
                case GameState.Ready:
                    Ready();
                    break;
                case GameState.Game:
                    Game();
                    break;
                case GameState.Adventure:
                    Adventure();
                    break;
                case GameState.Result:
                    Result();
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// 初期化パートの時に呼ばれるコルーチン
        /// </summary>
        IEnumerator InitCoroutine()
        {
            _scoreManager.Init();
            _timeManager.Init();
            _gameProgressManager.Init();
            
            _playerCore.Initialize(new PlayerParameter(), _playerDefaultTransform);
            
            // 初期化処理が完了するまで待機
            yield return null;
            
            _currentState.Value = GameState.Ready;
        }
        
        /// <summary>
        /// 準備パートの時に呼ばれるメソッド
        /// </summary>
        void Ready()
        {
            _timeManager.StartGameReadyCountDown();
            _currentState.Value = GameState.Game;
        }

        /// <summary>
        /// ゲームパートの処理
        /// </summary>
        void Game()
        {
            Debug.Log("Game Start");
        }
        
        /// <summary>
        /// アドベンチャーパートの処理
        /// </summary>
        void Adventure()
        {
            Debug.Log("ADV Start");
        }

        /// <summary>
        /// リザルトパートの処理
        /// </summary>
        void Result()
        {
            Debug.Log("Result Start");  
        }
        
        public ItemEffectContext CreateItemEffectContext()
        {
            return new ItemEffectContext
            {
                Player = _playerCore,
                ScoreManager = _scoreManager
            };
        }
    }
}
