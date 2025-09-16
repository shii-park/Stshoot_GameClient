using R3;
using StShoot.InGame.Enemies;
using UnityEngine;

namespace StShoot.InGame.Enemies.EnemyBulletInstance
{
    public class SmallEnemy : BaseEnemy
    {
        protected override void Start()
        {
            _hitPoint.Value = 1;
            _isAlive.Value = true;
            Debug.Log(_isAlive.Value);
            
            HitPoint.Subscribe(hp =>
            {
                if (hp <= 0)
                {
                    Die();
                }
            });
        }
        
        /// <summary>
        /// ダメージを受け取るメソッド
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public override void TakeDamage(int damage)
        {
            _hitPoint.Value -= damage;
        }

        protected override void Die()
        {
            Debug.Log("hoge");
            _isAlive.Value = false;
        }
    }
}
