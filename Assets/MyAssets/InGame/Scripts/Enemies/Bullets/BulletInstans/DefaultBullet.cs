using System.Collections;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets.BulletInstans
{
    public class DefaultBullet : BaseEnemyBullet
    {
        private bool _isMoving;
        
        protected override float Speed => 5f;
        
        public override void Move(Vector3 direction)
        {
            _isMoving = true;
            StartCoroutine(MoveBulletCoroutine(direction));
        }
        
        /// <summary>
        /// 弾を止めるメソッド
        /// </summary>
        public void StopBullet(){
            _isMoving = false;
        }
        
        IEnumerator MoveBulletCoroutine(Vector3 direction)
        {
            while (_isMoving && MainGameManager.Instance.CurrentGameState.CurrentValue == GameState.Game)
            {
                var pos = this.gameObject.transform.position;
                pos += direction * Speed * 0.1f;
                this.gameObject.transform.position = pos;

                yield return new WaitForSeconds(0.01f);
            }
        }
        
        /// <summary>
        /// 弾の利用可能状態を設定するメソッド
        /// </summary>
        /// <param name="isAvailable">Trueだったら利用可能、Falseだったら利用不可</param>
        public override void SetAvailable(bool isAvailable){
            _isAvailable.Value = isAvailable;
        }
    }
}
