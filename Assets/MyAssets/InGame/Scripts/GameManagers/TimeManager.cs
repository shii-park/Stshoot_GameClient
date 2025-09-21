using System.Collections;
using UnityEngine;
using R3;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// ゲームの時間管理を行うクラス
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        
        [SerializeField]
        private ReactiveProperty<float> _readySecond = new ReactiveProperty<float>(3.0f);

        [SerializeField]
        private ReactiveProperty<float> _progressSecond = new ReactiveProperty<float>(0);
        
        /// <summary>
        /// ゲーム開始前のカウントダウン
        /// </summary>
        public ReadOnlyReactiveProperty<float> ReadySecond => _readySecond;
        
        /// <summary>
        /// ゲームの進行時間
        /// </summary>
        public ReadOnlyReactiveProperty<float> ProgressSecond => _progressSecond;

        private bool _countProqressTimer;
        
        private MainGameManager _mainGameManager;
        
        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public void Init()
        {
            _progressSecond.Value = 0;
            _countProqressTimer = false;
            
            _mainGameManager = MainGameManager.Instance;
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// ゲーム開始前の待機タイマーを開始するメソッド
        /// </summary>
        public void StartGameReadyCountDown()
        {
            StartCoroutine(ReadyCountCoroutine());
        }

        /// <summary>
        /// ゲーム開始前の待機タイマーのコルーチン
        /// </summary>
        IEnumerator ReadyCountCoroutine()
        {
            while (_readySecond.Value > 0)
            {
                _readySecond.Value -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        /// <summary>
        /// ゲームの進行タイマーを開始するメソッド
        /// </summary>
        public void StartProgressTimer()
        {
            _countProqressTimer = true;
            StartCoroutine(GameCountUpCoroutine());
        }
        
        /// <summary>
        /// ゲームの進行タイマーのコルーチン
        /// </summary>
        IEnumerator GameCountUpCoroutine()
        {
            _readySecond.Value = 3.0f;
            
            while (_countProqressTimer)
            {
                yield return new WaitForSeconds(0.1f);
                _progressSecond.Value += 0.1f;
            }
        }
        
        /// <summary>
        /// ゲームの進行タイマーを停止するメソッド
        /// </summary>
        public void StopProgressTimer()
        {
            _countProqressTimer = false;
        }
        
        /// <summary>
        /// ゲームの進行タイマーをリセットするメソッド
        /// </summary>
        public void ResetProgressTimer()
        {
            _progressSecond.Value = 0;
        }
    }
}
