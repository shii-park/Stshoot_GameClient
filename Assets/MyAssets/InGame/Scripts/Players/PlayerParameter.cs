using System;

namespace StShoot.InGame.Players
{
    /// <summary>
    /// プレイヤーのパラメータを管理するクラス
    /// </summary>
    [Serializable]
    public class PlayerParameter
    {
        /// <summary>
        /// プレイヤーの残機
        /// 敵の攻撃を受けると1ずつ減っていく
        /// </summary>
        public int LifePoint;
        
        public readonly int MaxLifePoint = 2;
        
        public int PlayerPower;
        
        public readonly int MaxPlayerPower = 128;
        
        public PlayerParameter(int lifePoint = 2, int playerPower = 1)
        {
            LifePoint = lifePoint;
            PlayerPower = playerPower;
        }
    }
}
