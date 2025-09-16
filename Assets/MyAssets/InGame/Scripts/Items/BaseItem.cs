using R3;
using UnityEngine;

namespace StShoot.InGame.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        protected ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);
        /// <summary>
        /// 利用可能状態のプロパティ
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsAvailable => _isAvailable;
        
        public virtual void Init()
        {
            _isAvailable.Value = true;
        }
    }
}
