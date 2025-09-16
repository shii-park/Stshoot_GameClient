using StShoot.InGame.GameManagers;
using StShoot.InGame.Players;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテム効果のコンテキスト情報を保持するクラス
    /// </summary>
    public class ItemEffectContext
    {
        public PlayerCore Player { get; set; }
        public ScoreManager ScoreManager { get; set; }
    }
}
