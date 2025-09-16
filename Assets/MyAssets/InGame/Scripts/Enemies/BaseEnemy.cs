using R3;
using StShoot.InGame.Common.Interfaces;
using StShoot.InGame.Enemys.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーの基底クラス
    /// </summary>
    public abstract class BaseEnemy : MonoBehaviour, IDamageable, IEnemy
    {
        protected ReactiveProperty<int> _hitPoint;
        public virtual ReactiveProperty<int> HitPoint => _hitPoint;
        
        protected ReactiveProperty<bool> _isAlive = new ReactiveProperty<bool>();
        public virtual ReactiveProperty<bool> IsAlive => _isAlive;
        
        /// <summary>
        /// ダメージを受け取るメソッド
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public void TakeDamage(int damage)
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
        protected abstract void Die();
    }
}
