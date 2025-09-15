using UnityEngine;
using TMPro;

namespace StShoot.InGame.Players.Bullets
{
    /// <summary>
    /// プレイヤーの弾のビュー
    /// </summary>
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        TextMeshPro _countText;
        

        
        /// <summary>
        /// 弾のテキストを設定するメソッド
        /// </summary>
        /// <param name="commentChar">文字を入れる、一文字のみ</param>
        public void SetText(string commentChar){
            _countText.text = commentChar;
        }

        /// <summary>
        /// 弾の表示・非表示を切り替えるメソッド
        /// </summary>
        /// <param name="isActive">Trueだったら見える、Falseだったら見えない</param>
        public void SetActive(bool isActive){
            gameObject.SetActive(isActive);
        }
    }
}
