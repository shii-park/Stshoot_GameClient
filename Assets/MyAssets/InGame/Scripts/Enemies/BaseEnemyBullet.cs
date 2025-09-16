using UnityEngine;

namespace StShoot
{
    public abstract class BaseEnemyBullet : MonoBehaviour
    {
        protected abstract float Speed { get; }
        
        [SerializeField]
        protected GameObject enemyBullet;

        public virtual void Move(Vector2 direction)
        {
            transform.Translate(direction * Speed * Time.deltaTime);
        }
    }
}
