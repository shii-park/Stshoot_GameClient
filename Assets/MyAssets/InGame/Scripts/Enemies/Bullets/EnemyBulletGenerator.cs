using UnityEngine;

namespace StShoot.InGame.Enemies.Bullets
{
    /// <summary>
    /// エネミーの弾の制御を行うクラス
    /// </summary>
    public class EnemyBulletGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject _enemyBullet;
        
        /// <summary>
        /// エネミーの弾を発射するメソッド
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="direction">発射方向</param>
        public void ShotEnemyBullet(Vector2 position, Vector2 direction)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);
            if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)return;
            GameObject bullet = EnemyBulletFactory.Instance.Create(_enemyBullet.name, position);
            
            bullet?.GetComponent<BaseEnemyBullet>().Move(direction.normalized);
        }
    }
}
