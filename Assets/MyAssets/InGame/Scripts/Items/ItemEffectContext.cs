using StShoot.InGame.GameManagers;
using StShoot.InGame.Players;

namespace StShoot.InGame.Items
{
    public class ItemEffectContext
    {
        public PlayerCore Player { get; set; }
        public ScoreManager ScoreManager { get; set; }
    }
}
