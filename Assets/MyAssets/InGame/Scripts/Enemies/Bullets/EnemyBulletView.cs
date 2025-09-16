using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    /// <summary>
    /// エネミーの弾のビュークラス
    /// </summary>
    public class EnemyBulletView : MonoBehaviour
    {
        /// <summary>
        /// 弾の表示・非表示を切り替えるメソッド
        /// </summary>
        /// <param name="isActive">Trueだったら見える、Falseだったら見えない</param>
        public void SetActive(bool isActive){
            gameObject.SetActive(isActive);
        }
    }
}
