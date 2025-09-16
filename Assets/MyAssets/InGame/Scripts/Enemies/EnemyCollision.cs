using StShoot.InGame.GameManagers.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーの当たり判定を管理するクラス
    /// </summary>
    public class EnemyCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IKillable>()?.Kill();
        }
    }
}
