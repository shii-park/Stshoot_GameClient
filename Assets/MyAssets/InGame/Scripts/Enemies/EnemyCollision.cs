using StShoot.InGame.GameManagers.Interfaces;
using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーの当たり判定を管理するクラス
    /// </summary>
    public class EnemyCollision : MonoBehaviour
    {

        [SerializeField] 
        private EnemyPresenter _presenter;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IKillable>()?.Kill();
        }
    }
}
