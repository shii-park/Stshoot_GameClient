using System.Collections.Generic;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        public static EnemyFactory Instance { get; private set; }

        [SerializeField]
        private List<GameObject> _enemyPrefabs;

        private Dictionary<string, List<GameObject>> _enemyPools = new Dictionary<string, List<GameObject>>();

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

            foreach (var prefab in _enemyPrefabs)
            {
                _enemyPools.Add(prefab.name, new List<GameObject>());
            }
        }

        public GameObject Create(string enemyName, Vector3 spawnPosition)
        {
            if (!_enemyPools.ContainsKey(enemyName))
            {
                Debug.LogError($"EnemyFactory: 指定された敵の名前が存在しません。{enemyName}");
                return null;
            }

            foreach (var enemy in _enemyPools[enemyName])
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                    return enemy;
                }
            }

            var prefab = _enemyPrefabs.Find(p => p.name == enemyName);
            if (prefab == null)
            {
                Debug.LogError($"EnemyFactory: 指定された敵の名前が存在しません。{enemyName}");
                return null;
            }

            var newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            _enemyPools[enemyName].Add(newEnemy);
            return newEnemy;
        }
    }
}