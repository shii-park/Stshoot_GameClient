using R3;
using UnityEngine;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテムの基底クラス
    /// </summary>
    public abstract class BaseItem : MonoBehaviour
    {
        protected ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);
        /// <summary>
        /// 利用可能状態のプロパティ
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsAvailable => _isAvailable;
        
        [SerializeField]
        protected ItemMove _itemMove;
        
        /// <summary>
        /// アイテムの初期化メソッド
        /// </summary>
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
        
        /// <summary>
        /// アイテムの効果を適用するメソッド
        /// </summary>
        /// <param name="context">アイテム効果のコンテキスト情報</param>
        public virtual void ApplyEffect(ItemEffectContext context){ }
    }
}
