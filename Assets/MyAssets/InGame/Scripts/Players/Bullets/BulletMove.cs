using UnityEngine;
using System.Collections;

namespace StShoot.InGame.Players.Bullets
{
    /// <summary>
    /// プレイヤーの弾の移動を制御するクラス
    /// </summary>
    public class BulletMove : MonoBehaviour
    {
        private bool _isMoving;

        [SerializeField]
        private float _bulletSpeed;

        /// <summary>
        /// 弾を移動させるメソッド
        /// </summary>
        public void MoveBullet(){
            _isMoving = true;
            StartCoroutine(MoveBulletCoroutine());
        }

        /// <summary>
        /// 弾を止めるメソッド
        /// </summary>
        public void StopBullet(){
            _isMoving = false;
        }

        /// <summary>
        /// 弾の移動を制御するコルーチン
        /// </summary>
        IEnumerator MoveBulletCoroutine()
        {
            // 条件にゲームの状態を増やす
            while (_isMoving)
            {
                var pos = this.gameObject.transform.position;
                pos.y += 0.1f * _bulletSpeed;
                this.gameObject.transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}