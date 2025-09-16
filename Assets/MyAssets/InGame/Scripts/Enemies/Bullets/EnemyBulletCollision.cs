using StShoot.InGame.GameManagers.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    public class EnemyBulletCollision : MonoBehaviour
    {
        [SerializeField]
        private BaseEnemyBullet _model;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            if (collisionObject.GetComponent<IWall>() != null)
            {
                _model.SetAvailable(true);
            }
            else if (collisionObject.GetComponent<IKillable>() != null )
            {
                collisionObject.GetComponent<IKillable>().Kill();
                
                _model.SetAvailable(true);
            }
        }
    }
}
