using UnityEngine;

namespace StShoot.InGame.Enemies
{
    public enum MoveType
    {
        Straight,
        Curve,
        Wave
    }

    [System.Serializable]
    public class Waypoint
    {
        public Vector3 Position;
        public float Duration;
        public MoveType MoveType;
    }
}