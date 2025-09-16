using System.Collections;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets.BulletInstance
{
    /// <summary>
    /// デフォルトの弾のクラス
    /// </summary>
    public class DefaultBullet : BaseEnemyBullet
    {
        private bool _isMoving;
        
        protected override float Speed => 0.7f;
        
        public override void Move(Vector3 direction)
        {
            _isMoving = true;
            SetAvailable(false);
            StartCoroutine(MoveBulletCoroutine(direction));
        }
        
        /// <summary>
        /// 弾を止めるメソッド
        /// </summary>
        public void StopBullet(){
            _isMoving = false;
        }
        
        /// <summary>
        /// 弾を動かすコルーチン
        /// </summary>
        /// <param name="direction">方向</param>
        IEnumerator MoveBulletCoroutine(Vector3 direction)
        {
            Vector3 prePosition = this.gameObject.transform.position;
            while (_isMoving && MainGameManager.Instance.CurrentGameState.CurrentValue == GameState.Game)
            {
                var pos = this.gameObject.transform.position;
                pos += direction.normalized * Speed * 0.1f;

                if (prePosition == pos) Destroy(this.gameObject);
                prePosition = pos;
                
                this.gameObject.transform.position = pos;
                
                yield return new WaitForSeconds(0.01f);
            }

            SetAvailable(true);
        }
        
        /// <summary>
        /// 弾の利用可能状態を設定するメソッド
        /// </summary>
        /// <param name="isAvailable">Trueだったら利用可能、Falseだったら利用不可</param>
        public override void SetAvailable(bool isAvailable){
            if (isAvailable)
            {
                StopBullet();
            }
            _isAvailable.Value = isAvailable;
        }
    }
}
