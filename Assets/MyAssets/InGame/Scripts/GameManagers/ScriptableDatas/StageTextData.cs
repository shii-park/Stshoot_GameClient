using UnityEngine;

namespace StShoot.InGame.GameManagers.ScriptableDatas
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
    public class StageTextData : ScriptableObject
    {
        public string StageTitle;      // ステージタイトル
        public string StageBGMTitle;      // 道中曲のタイトル名
        public string BossBGMTitle;      // 道中曲のタイトル名
    }
}
