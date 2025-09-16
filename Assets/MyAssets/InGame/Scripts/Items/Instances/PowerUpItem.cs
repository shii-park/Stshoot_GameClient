using UnityEngine;

namespace StShoot.InGame.Items.Instances
{
    public class PowerUpItem : BaseItem
    {
        private int _powerUpPoint = 1;
        
        public override void Init()
        {
            _isAvailable.Value = true;
        }

        public override void ApplyEffect(ItemEffectContext context)
        {
            context.Player.IncreasePower(_powerUpPoint);
        }
    }
}
