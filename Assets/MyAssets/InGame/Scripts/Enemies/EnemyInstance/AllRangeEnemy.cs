using System.Collections;
using R3;
using StShoot.InGame.Items;
using UnityEngine;

namespace StShoot.InGame.Enemies.EnemyInstance
{
    /// <summary>
    /// 全方位攻撃のエネミークラス
    /// </summary>
    public class AllRangeEnenmy : BaseEnemy
    {
        public override void Init()
        {
            _hitPoint.Value = 1;
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
            int bulletCount = 12;
            float angleStep = 360f / bulletCount;
            while (true)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
                    _enemyBulletGenerator.ShotEnemyBullet(this.transform.position, direction);
                }
                
                yield return new WaitForSeconds(1f);
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
