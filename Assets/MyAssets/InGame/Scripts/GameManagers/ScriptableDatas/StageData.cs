using UnityEngine;

namespace StShoot.InGame.GameManagers.ScriptableDatas
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
    public class StageTextData : ScriptableObject
    {
        public string stageTitle;      // ステージタイトル
        public string musicTitle;      // 曲のタイトル名
    }
}
