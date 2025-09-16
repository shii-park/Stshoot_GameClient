using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーの弾の制御を行うクラス
    /// </summary>
    public class EnemyBulletGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject _enemyBullet;
        
        public void ShotEnemyBullet(Vector2 position, Vector2 direction)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);
            if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)return;
            GameObject bullet = Instantiate(_enemyBullet, position, Quaternion.identity);
            
            bullet?.GetComponent<BaseEnemyBullet>().Move(direction.normalized);
        }
    }
}
