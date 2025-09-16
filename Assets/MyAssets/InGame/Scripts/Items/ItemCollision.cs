using UnityEngine;

namespace StShoot.InGame.Items
{
    public class ItemCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
            }
        }
    }
}
