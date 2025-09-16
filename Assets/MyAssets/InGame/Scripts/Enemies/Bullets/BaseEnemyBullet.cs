using R3;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    /// <summary>
    /// エネミーの弾の基底クラス
    /// </summary>
    public abstract class BaseEnemyBullet : MonoBehaviour
    {
        protected abstract float Speed { get; }
        
        protected ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);

        /// <summary>
        /// 利用可能状態のプロパティ
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsAvailable => _isAvailable;

        public virtual void Move(Vector3 direction)
        {
            transform.Translate(direction * Speed * Time.deltaTime);
        }
        /// <summary>
        /// 弾の利用可能状態を設定するメソッド
        /// </summary>
        /// <param name="isAvailable">Trueだったら利用可能、Falseだったら利用不可</param>
        public virtual void SetAvailable(bool isAvailable){
            _isAvailable.Value = isAvailable;
        }
    }
}
