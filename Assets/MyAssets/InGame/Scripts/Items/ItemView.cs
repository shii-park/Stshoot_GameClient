using UnityEngine;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテムのビュークラス
    /// </summary>
    public class ItemView : MonoBehaviour
    {
        /// <summary>
        /// アイテムの表示・非表示を切り替えるメソッド
        /// </summary>
        /// <param name="isActive">Trueだったら見える、Falseだったら見えない</param>
        public void SetActive(bool isActive){
            gameObject.SetActive(isActive);
        }
    }
}
