using System.Collections;
using System.Collections.Generic;
using StShoot.InGame.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// ゲームの進行を管理するクラス
    /// </summary>
    public class GameProgressManager : MonoBehaviour
    {
        public static GameProgressManager Instance { get; private set; }

        private MainGameManager _mainGameManager;
        
        [SerializeField] private List<GameObject> _enemies;
        
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
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 道中の敵の動きなどを処理するメソッド
        /// </summary>
        public void ProgressStage()
        {
            StartCoroutine(ProgressStageCoroutine());
        }

        /// <summary>
        /// 敵の動きを作るコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator ProgressStageCoroutine()
        {
            yield return new WaitForSeconds(3f);
            while (_mainGameManager.CurrentGameState.CurrentValue == GameState.Game)
            {
                EnemyFactory.Instance.Create(
                    _enemies[2].name, 
                    new Vector3(0f, 6f, -1f), new List<Waypoint>
                    {
                        new Waypoint(new Vector3(3f, 0f, -1f), 5f, MoveType.Straight),
                        new Waypoint(new Vector3(-3f, 2f, -1f), 5f, MoveType.Straight),
                        new Waypoint(new Vector3(-3f, -3f, -1f), 5f, MoveType.Straight),
                        new Waypoint(new Vector3(3f, 3f, -1f), 5f, MoveType.Straight),
                        new Waypoint(new Vector3(0f, 0f, -1f), 5f, MoveType.Straight),
                        new Waypoint(new Vector3(0f, 0f, -1f), 5f, MoveType.Straight),
                    });
                yield return new WaitForSeconds(30f);
                
                for (int i = 0; i < 10; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[0].name, 
                        new Vector3(-4.5f, 3.6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(4.5f, 3.6f, -1f), 7f, MoveType.Straight),
                        });
                
                    EnemyFactory.Instance.Create(
                        _enemies[0].name, 
                        new Vector3(4.5f, 1.8f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(-4.5f, 1.8f, 0f), 7f, MoveType.Straight),
                        });
                    yield return new WaitForSeconds(1f);
                }
                
                yield return new WaitForSeconds(3f);
                
                for (int i = 0; i < 5; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[1].name, 
                        new Vector3(-3f, 6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(5f, 0f, -1f), 4f, MoveType.CurveOuter),
                        });
                    yield return new WaitForSeconds(1f);
                }
                
                yield return new WaitForSeconds(2f);
                
                for (int i = 0; i < 5; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[1].name, 
                        new Vector3(3f, 6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(-5f, 0f, -1f), 4f, MoveType.CurveInner),
                        });
                    yield return new WaitForSeconds(1f);
                }
                
                yield return new WaitForSeconds(3f);
                
                for (int i = 0; i < 10; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[1].name, 
                        new Vector3(2.5f, 6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(2.5f, -6f, -1f), 2.5f, MoveType.WaveX),
                        });
                    yield return new WaitForSeconds(0.5f);
                }
                
                yield return new WaitForSeconds(3f);
                
                for (int i = 0; i < 10; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[1].name, 
                        new Vector3(-4.5f, 3.6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(4.5f, 3.6f, -1f), 2.5f, MoveType.WaveY),
                        });
                    yield return new WaitForSeconds(0.5f);
                }
                
                yield return new WaitForSeconds(3f);
                
                for (int i = 0; i < 10; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[1].name, 
                        new Vector3(-2.5f, -6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(-2.5f, 6f, -1f), 2.5f, MoveType.WaveX),
                        });
                    yield return new WaitForSeconds(0.5f);
                }
                
                yield return new WaitForSeconds(3f);
                
                for (int i = 0; i < 150; i++)
                {
                    EnemyFactory.Instance.Create(
                        _enemies[3].name, 
                        new Vector3(Random.Range(-2.5f, 2.5f), 6f, -1f), new List<Waypoint>
                        {
                            new Waypoint(new Vector3(Random.Range(-3f, 3f), -7f, -1f), 2f, MoveType.Straight),
                        });
                    yield return new WaitForSeconds(0.1f);
                }
                
                yield return new WaitForSeconds(3f);
                
                
                yield return new WaitForSeconds(3f);
                

                
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
