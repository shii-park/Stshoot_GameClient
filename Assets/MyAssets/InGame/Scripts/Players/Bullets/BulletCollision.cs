using StShoot.InGame.Scripts.Common.Interfaces;
using StShoot.InGame.Scripts.Enemys.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Scripts.Players.Bullets
{
    /// <summary>
    /// プレイヤーの弾の当たり判定を管理するクラス
    /// </summary>
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField]
        private BulletPresenter _presenter;
        
        private BulletModel _model => _presenter.Model;
        
        /// <summary>
        /// 弾の当たり判定
        /// 壁またはエネミーに当たったら利用可能状態にする
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            if (collisionObject.GetComponent<IWall>() != null)
            {
                _model.SetAvailable(true);
            }
            else if (collisionObject.GetComponent<IEnemy>() != null )
            {
                collisionObject.GetComponent<IDamageable>()?.TakeDamage(_model.BulletPower);
                _model.SetAvailable(true);
            }
        }
    }
}