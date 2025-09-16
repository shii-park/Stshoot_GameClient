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
        
        [SerializeField]
        protected ItemMove _itemMove;
        
        public virtual void Init()
        {
            _isAvailable.Value = false;
            _itemMove.Init();
        }
        
        /// <summary>
        /// アイテムの利用可能状態を設定するメソッド
        /// </summary>
        /// <param name="isAvailable">Trueだったら利用可能、Falseだったら利用不可</param>
        public virtual void SetAvailable(bool isAvailable){
            _isAvailable.Value = isAvailable;
        }
        
        public virtual void ApplyEffect(ItemEffectContext context){ }
    }
}
