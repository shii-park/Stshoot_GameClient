using UnityEngine;

namespace StShoot.InGame.Items.Instances
{
    public class ScoreUpItem : BaseItem
    {
        private int _scoreUpPoint = 20000;
        
        public override void Init()
        {
            _isAvailable.Value = true;
        }
        
        public override void ApplyEffect(ItemEffectContext context)
        {
            context.ScoreManager.AddScore(_scoreUpPoint);
        }
    }
}
