using System.Collections;
using R3;
using StShoot.InGame.Items;
using UnityEngine;

namespace StShoot.InGame.Enemies.EnemyInstance
{
    /// <summary>
    /// ランダム方向モンスターのクラス
    /// </summary>
    public class RandomEnemy : BaseEnemy
    {
        [SerializeField]
        private float _shotInterval = 0.01f;
        public override void Init()
        {
            _hitPoint.Value = 200;
            _isAlive.Value = true;
            
            var disposable = new SingleAssignmentDisposable();
            
            disposable.Disposable = HitPoint.Subscribe(hp =>
            {
                if (hp <= 0)
                {
                    disposable.Dispose();
                    Die();
                }
            });

            StartCoroutine(ShotCoroutine());
        }
        
        private IEnumerator ShotCoroutine()
        {
            while (true)
            {
                _enemyBulletGenerator.ShotEnemyBullet(this.transform.position, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized);
                yield return new WaitForSeconds(_shotInterval);
            }
        }
        
        /// <summary>
        /// ダメージを受け取るメソッド
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public override void TakeDamage(int damage)
        {
            _hitPoint.Value -= damage;
        }

        public override void Die()
        {
            ItemFactory.Instance.CreateRandom(this.transform.position);
            _isAlive.Value = false;
        }
    }
}
