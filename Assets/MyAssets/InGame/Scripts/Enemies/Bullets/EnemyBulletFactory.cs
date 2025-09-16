using System.Collections.Generic;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    public class EnemyBulletFactory : MonoBehaviour
    {
        public static EnemyBulletFactory Instance { get; private set; }
        
        [SerializeField]
        private List<GameObject> _bulletPrefabs;
        
        private Dictionary<string, List<GameObject>> _bulletFactories = new Dictionary<string, List<GameObject>>();
        private GameObject _bulletsParent; // 管理用の親

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

            // 管理用の親オブジェクトを生成
            _bulletsParent = new GameObject("BulletsParent");

            foreach (var prefab in _bulletPrefabs)
            {
                _bulletFactories.Add(prefab.name, new List<GameObject>());
            }
        }
        
        public GameObject Create(string bulletName, Vector3 genePosition)
        {
            if(MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game) return null;
            if (!_bulletFactories.ContainsKey(bulletName))
            {
                Debug.LogError($"BulletFactory: 指定された弾の名前が存在しません。{bulletName}");
                return null;
            }

            foreach (var bullet in _bulletFactories[bulletName])
            {
                if (bullet.GetComponent<BaseEnemyBullet>().IsAvailable.CurrentValue)
                {
                    bullet.transform.SetParent(_bulletsParent.transform, false);
                    bullet.transform.position = genePosition;
                    bullet.SetActive(true);
                    return bullet;
                }
            }

            var prefab = _bulletPrefabs.Find(p => p.name == bulletName);
            if (prefab == null)
            {
                Debug.LogError($"BulletFactory: 指定された弾の名前が存在しません。{bulletName}");
                return null;
            }

            var newBullet = Instantiate(prefab, genePosition, Quaternion.identity, _bulletsParent.transform);
            _bulletFactories[bulletName].Add(newBullet);
            return newBullet;
        }
    }
}
