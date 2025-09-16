using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーのビュークラス
    /// </summary>
    public class EnemyView : MonoBehaviour
    {
        /// <summary>
        /// エネミーの表示・非表示を切り替えるメソッド
        /// </summary>
        /// <param name="isActive">Trueだったら見える、Falseだったら見えない</param>
        public void SetActive(bool isActive){
            gameObject.SetActive(isActive);
        }
    }
}
