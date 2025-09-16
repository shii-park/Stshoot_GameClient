using System.Collections.Generic;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    public class EnemyBulletFactory : MonoBehaviour
    {
        public static EnemyBulletFactory Instance { get; private set; }
        
        [SerializeField]
        private List<GameObject> _bulletPrefabs;
        
        Dictionary<string, List<GameObject>> _bulletFactories = new Dictionary<string, List<GameObject>>();
        
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
            
            foreach (var prefab in _bulletPrefabs)
            {
                _bulletFactories.Add(prefab.name, new List<GameObject>());
            }
        }
        
        public GameObject Create(string bulletName, Vector3 genePosition)
        {
            if (_bulletFactories.ContainsKey(bulletName) == false)
            {
                Debug.LogError($"BulletFactory: 指定された弾の名前が存在しません。{bulletName}");
                return null;
            }

            // 使われていない弾を探す
            foreach (var bullet in _bulletFactories[bulletName])
            {
                if (bullet.GetComponent<BaseEnemyBullet>().IsAvailable.CurrentValue)
                {
                    bullet.transform.position = genePosition;
                    bullet.SetActive(true);
                    return bullet;
                }
            }

            // 使われていない弾がなかった場合、新しく生成する
            var prefab = _bulletPrefabs.Find(p => p.name == bulletName);
            if (prefab == null)
            {
                Debug.LogError($"BulletFactory: 指定された弾の名前が存在しません。{bulletName}");
                return null;
            }

            var newBullet = Instantiate(prefab, genePosition, Quaternion.identity);
            _bulletFactories[bulletName].Add(newBullet);
            return newBullet;
        }
    }
}
