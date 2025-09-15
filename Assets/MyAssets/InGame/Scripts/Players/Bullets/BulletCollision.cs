using MyAssets.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace MyAssets.InGame.Scripts.Players.Bullets
{

    public class BulletCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IWall>() != null)
            {
                Debug.Log("hogehoge");
            }
        }
    }
}