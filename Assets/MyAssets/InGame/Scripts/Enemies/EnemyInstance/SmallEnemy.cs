using System.Collections;
using R3;
using StShoot.InGame.Items;
using UnityEngine;

namespace StShoot.InGame.Enemies.EnemyInstance
{
    /// <summary>
    /// 小型エネミーのクラス
    /// </summary>
    public class SmallEnemy : BaseEnemy
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
            while (true)
            {
                _enemyBulletGenerator.ShotEnemyBullet(this.transform.position, Vector3.down);
                yield return new WaitForSeconds(0.3f);
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
