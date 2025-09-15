using StShoot.InGame.Scripts.Common.Interfaces;
using StShoot.InGame.Scripts.Enemys.Interfaces;
using UnityEngine;

namespace StShoot
{
    public class Test2 : MonoBehaviour,IEnemy
    {
        public void TakeDamage(int damage)
        {
            Debug.Log($"ダメージ{damage}を受けた");
        }
    }
}
