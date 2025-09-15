using UnityEngine;

namespace StShoot.InGame.GameManagers.ScriptableDatas
{
    [CreateAssetMenu(fileName = "StageDatabase", menuName = "Scriptable Objects/StageDatabase")]
    public class StageDatabase : ScriptableObject
    {
        public StageTextData[] stages;     // ステージデータの配列
    }
}
