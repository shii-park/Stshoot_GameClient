using UnityEngine;

namespace StShoot.InGame.Items.Instances
{
    /// <summary>
    /// スコアアップアイテムのクラス
    /// </summary>
    public class ScoreUpItem : BaseItem
    {
        private int _scoreUpPoint = 20000;
        
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
            context.ScoreManager.AddScore(_scoreUpPoint);
            SetAvailable(true);
        }
    }
}
