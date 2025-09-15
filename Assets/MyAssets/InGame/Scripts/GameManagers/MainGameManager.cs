using System.Collections;
using R3;
using UnityEngine;
using UnityEngine.Events;

namespace StShoot.InGame.GameManagers
{
    public class MainGameManager : MonoBehaviour
    {
        public static MainGameManager Instance { get; private set; }
        
        private GameStateReactiveProperty _currentState = new GameStateReactiveProperty(GameState.Init);

        public ReadOnlyReactiveProperty<GameState> CurrentGameState => _currentState;
        
        [SerializeField]
        private TimeManager _timeManager;
        
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
        }
        
        public void SetGameState(GameState nextState)
        {
            _currentState.Value = nextState;
        }
        
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
        IEnumerator InitCoroutine()
        {
            yield return null;
            
            _currentState.Value = GameState.Ready;
        }
        void Ready()
        {
            _timeManager.StartGameReadyCountDown();
            _currentState.Value = GameState.Game;
        }

        void Game()
        {
            // ゲームパートの処理
        }
        void Adventure()
        {
            // アドベンチャーパートの処理
        }

        void Result()
        {
            // リザルトの処理    
        }
    }
}
