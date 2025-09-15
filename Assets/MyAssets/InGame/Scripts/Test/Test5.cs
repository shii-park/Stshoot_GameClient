using StShoot.InGame.GameManagers.Interfaces;
using UnityEngine;

namespace StShoot
{
    public class Test5 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IKillable>()?.Kill();
        }
    }
}
