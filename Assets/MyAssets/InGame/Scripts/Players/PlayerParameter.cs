using System;
using UnityEngine;

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
        public int LifePoint = 2;
        
        public readonly int MaxLifePoint = 2;
        
        public int PlayerPower = 1;
        
        public readonly int MaxPlayerPower = 128;
    }
}
