using System.Collections;
using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies.EnemyInstance
{
    public class TrackingEnemy : BaseEnemy
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
                var myPos = this.transform.position;
                var playerPos = MainGameManager.Instance.PlayerPosition.position;
                var direction = (playerPos - myPos).normalized;
                
                _enemyBulletGenerator.ShotEnemyBullet(myPos, direction);
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
            _isAlive.Value = false;
        }
    }
}
