using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// 移動タイプ
    /// </summary>
    public enum MoveType
    {
        Straight,
        Curve,
        CurveInner,
        CurveOuter,
        WaveX,
        WaveY
    }

    /// <summary>
    /// ウェイポイントのデータクラス
    /// </summary>
    [System.Serializable]
    public class Waypoint
    {
        public Vector3 Position;
        public float Duration;
        public MoveType MoveType;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Waypoint(Vector3 position, float duration, MoveType moveType)
        {
            Position = position;
            Duration = duration;
            MoveType = moveType;
        }
    }
}