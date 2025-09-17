using UnityEngine;

namespace StShoot.InGame.Items.Instances
{
    /// <summary>
    /// パワーアップアイテムのクラス
    /// </summary>
    public class PowerUpItem : BaseItem
    {
        private int _powerUpPoint = 10;
        
        /// <summary>
        /// アイテムの初期化メソッド
        /// </summary>
        public override void Init()
        {
            _isAvailable.Value = false;
            _itemMove.Init();
        }

        /// <summary>
        /// アイテムの効果を適用するメソッド
        /// </summary>
        /// <param name="context">アイテム効果のコンテキスト情報</param>
        public override void ApplyEffect(ItemEffectContext context)
        {
            context.Player.IncreasePower(_powerUpPoint);
            SetAvailable(true);
        }
    }
}
