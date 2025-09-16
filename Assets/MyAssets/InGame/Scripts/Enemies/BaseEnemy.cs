using R3;
using StShoot.InGame.Common.Interfaces;
using StShoot.InGame.Enemys.Interfaces;
using StShoot.InGame.Enemies.Bullets;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーの基底クラス
    /// </summary>
    public abstract class BaseEnemy : MonoBehaviour, IDamageable, IEnemy
    {
        protected ReactiveProperty<int> _hitPoint = new ReactiveProperty<int>();
        public ReactiveProperty<int> HitPoint => _hitPoint;
        
        protected ReactiveProperty<bool> _isAlive = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAlive => _isAlive;
        
        [SerializeField]
        protected EnemyBulletGenerator _enemyBulletGenerator;
        
        public virtual void Init()
        {
            _hitPoint.Value = 1;
            _isAlive.Value = true;
        }
        
        /// <summary>
        /// ダメージを受け取るメソッド
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public virtual void TakeDamage(int damage)
        {
            _hitPoint.Value -= damage;
            
            if (_hitPoint.Value <= 0)
            {
                Die();
            }
        }
        
        /// <summary>
        /// 死ぬ処理
        /// </summary>
        public abstract void Die();
    }
}
