using System.Collections;
using System.Collections.Generic;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    /// <summary>
    /// エネミーの弾のファクトリークラス
    /// </summary>
    public class EnemyBulletFactory : MonoBehaviour
    {
        public static EnemyBulletFactory Instance { get; private set; }

        [SerializeField]
        private List<GameObject> _bulletPrefabs;

        private Dictionary<string, List<GameObject>> _bulletFactories = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, GameObject> _bulletsParents = new Dictionary<string, GameObject>();
        
        private bool _canGenerate = true;

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

            foreach (var prefab in _bulletPrefabs)
            {
                _bulletFactories.Add(prefab.name, new List<GameObject>());
                var parent = new GameObject($"{prefab.name}_BulletsParent");
                _bulletsParents.Add(prefab.name, parent);
            }
        }

        /// <summary>
        /// 弾を生成するメソッド
        /// </summary>
        /// <param name="bulletName">生成したい弾の名前</param>
        ///  <param name="genePosition">生成したい位置</param>
        public GameObject Create(string bulletName, Vector3 genePosition)
        {
            if (!_canGenerate) return null;
            
            if(MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game) return null;
            if (!_bulletFactories.ContainsKey(bulletName))
            {
                Debug.LogError($"BulletFactory: 指定された弾の名前が存在しません。{bulletName}");
                return null;
            }

            var parentObj = _bulletsParents[bulletName];

            foreach (var bullet in _bulletFactories[bulletName])
            {
                if (bullet.GetComponent<BaseEnemyBullet>().IsAvailable.CurrentValue)
                {
                    bullet.transform.SetParent(parentObj.transform, false);
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

            var newBullet = Instantiate(prefab, genePosition, Quaternion.identity, parentObj.transform);
            _bulletFactories[bulletName].Add(newBullet);
            return newBullet;
        }
        
        /// <summary>
        /// 弾の生成を一時的に停止するメソッド
        /// </summary>
        /// <param name="seconds">停止時間</param>
        public void StopBulletGeneration(float seconds)
        {
            StartCoroutine(StopBulletGenerationCoroutine(seconds));
        }

        /// <summary>
        /// 弾の生成を一時的に停止するコルーチン
        /// </summary>
        /// <param name="seconds">停止時間</param>
        private IEnumerator StopBulletGenerationCoroutine(float seconds)
        {
            _canGenerate = false;
            yield return new WaitForSeconds(seconds);
            _canGenerate = true;
        }
        
        /// <summary>
        /// 全ての弾を非アクティブにするメソッド
        /// </summary>
        public void RemoveAllBullets()
        {
            foreach (var bulletList in _bulletFactories.Values)
            {
                foreach (var bullet in bulletList)
                {
                    bullet.SetActive(false);
                }
            }
        }
    }
}
