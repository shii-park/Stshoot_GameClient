using StShoot.InGame.Common.Interfaces;
using StShoot.InGame.Enemys.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace StShoot.InGame.Players.Bullets
{
    public class SuperBullet : MonoBehaviour
    {
        [SerializeField]
        private string _superText;

        private bool isAvailable;
        
        public bool IsAvailable => isAvailable;
        
        private int _bulletPower = 2;
        
        public void Set(string superText)
        {
            isAvailable = false;
            _superText = superText;
            this.gameObject.SetActive(true);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            if (collisionObject.GetComponent<IWall>() != null)
            {
                isAvailable = true;
                this.gameObject.SetActive(false);
            }
            else if (collisionObject.GetComponent<IEnemy>() != null )
            {
                collisionObject.GetComponent<IDamageable>()?.TakeDamage(_bulletPower);
            }
        }
    }
}
