using StShoot.InGame.Common.Interfaces;
using StShoot.InGame.Enemys.Interfaces;
using StShoot.InGame.GameManagers.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    public class EnemyBulletCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            if (collisionObject.GetComponent<IWall>() != null)
            {
                //_model.SetAvailable(true);
            }
            else if (collisionObject.GetComponent<IKillable>() != null )
            {
                collisionObject.GetComponent<IKillable>().Kill();
                
                
                //_model.SetAvailable(true);
            }
        }
    }
}
