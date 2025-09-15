using StShoot.InGame.Scripts.Common.Interfaces;
using StShoot.InGame.Scripts.Enemys.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Scripts.Players.Bullets
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField]
        private BulletPresenter _presenter;
        
        private BulletModel _model => _presenter.Model;
        
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            if (collisionObject.GetComponent<IWall>() != null)
            {
                _model.SetAvailabl(true);
            }
            else if (collisionObject.GetComponent<IEnemy>() != null )
            {
                collisionObject.GetComponent<IDamageable>()?.TakeDamage(_model.BulletPower);
                _model.SetAvailabl(true);
            }
        }
    }
}