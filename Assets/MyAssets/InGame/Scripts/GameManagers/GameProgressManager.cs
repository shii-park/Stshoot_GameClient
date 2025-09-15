using UnityEngine;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// ゲームの進行を管理するクラス
    /// </summary>
    public class GameProgressManager : MonoBehaviour
    {
        public static GameProgressManager Instance { get; private set; }

        private MainGameManager _mainGameManager;
        
        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public void Init()
        {
            _mainGameManager = MainGameManager.Instance;
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
        /// 道中の敵の動きなどを処理するメソッド
        /// </summary>
        private void ProgressStage()
        {
            // 道中の敵の動きやイベントをここで処理
        }
        
        /// <summary>
        /// ボス戦の進行を処理するメソッド
        /// </summary>
        private void ProgressBossStage()
        {
            // ボス戦の進行をここで処理
        }
    }
}
