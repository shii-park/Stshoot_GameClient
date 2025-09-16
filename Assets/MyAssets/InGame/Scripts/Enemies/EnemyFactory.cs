using System.Collections.Generic;
using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        public static EnemyFactory Instance { get; private set; }

        [SerializeField]
        private List<GameObject> _enemyPrefabs;

        private Dictionary<string, List<GameObject>> _enemyPools = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, GameObject> _enemiesParents = new Dictionary<string, GameObject>();

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
                return;
            }

            foreach (var prefab in _enemyPrefabs)
            {
                _enemyPools.Add(prefab.name, new List<GameObject>());
                var parent = new GameObject($"{prefab.name}_EnemiesParent");
                _enemiesParents.Add(prefab.name, parent);
            }
        }

        private void Start()
        {
            MainGameManager.Instance.CurrentGameState
                .Where(state => state != GameState.Game)
                .Subscribe(_ =>
                {
                    RemoveAllEnemies();
                });
        }

        public GameObject Create(string enemyName, Vector3 spawnPosition, List<Waypoint> waypoints)
        {
            if(MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game){ return null;}
            
            if (!_enemyPools.ContainsKey(enemyName))
            {
                Debug.LogError($"EnemyFactory: 指定された敵の名前が存在しません。{enemyName}");
                return null;
            }

            var parentObj = _enemiesParents[enemyName];
            GameObject enemy = null;
            foreach (var e in _enemyPools[enemyName])
            {
                if (!e.activeInHierarchy)
                {
                    enemy = e;
                    break;
                }
            }

            if (enemy == null)
            {
                var prefab = _enemyPrefabs.Find(p => p.name == enemyName);
                if (prefab == null)
                {
                    Debug.LogError($"EnemyFactory: 指定された敵の名前が存在しません。{enemyName}");
                    return null;
                }
                enemy = Instantiate(prefab, spawnPosition, Quaternion.identity, parentObj.transform);
                _enemyPools[enemyName].Add(enemy);
            }
            else
            {
                enemy.transform.SetParent(parentObj.transform, false);
                enemy.transform.position = spawnPosition;
                enemy.SetActive(true);
            }

            enemy.GetComponent<BaseEnemy>().Init();

            var movement = enemy.GetComponent<EnemyMovementController>();
            if (movement != null)
            {
                movement.SetWaypoints(waypoints);
            }

            return enemy;
        }
        
        public void RemoveAllEnemies()
        {
            foreach (var enemyPools in _enemyPools.Values)
            {
                foreach (var enemy in enemyPools)
                {
                    enemy.GetComponent<BaseEnemy>().Die();
                }
            }
        }
    }
}
