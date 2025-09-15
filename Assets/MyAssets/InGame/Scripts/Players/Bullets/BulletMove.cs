using UnityEngine;
using System.Collections;
using StShoot.InGame.GameManagers;

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
        public void MoveBullet(float angleDeg){
            _isMoving = true;
            StartCoroutine(MoveBulletCoroutine(angleDeg));
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
        IEnumerator MoveBulletCoroutine(float angleDeg)
        {
            // 角度をラジアンに変換
            float angleRad = angleDeg * Mathf.Deg2Rad;

            // 進行方向ベクトル
            Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);

            while (_isMoving && MainGameManager.Instance.CurrentGameState.CurrentValue == GameState.Game)
            {
                var pos = this.gameObject.transform.position;
                pos += direction * _bulletSpeed * 0.1f;
                this.gameObject.transform.position = pos;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}