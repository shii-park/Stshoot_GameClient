using StShoot.InGame.Common.Interfaces;
using StShoot.InGame.Enemys.Interfaces;
using UnityEngine;

namespace StShoot
{
    public class Test2 : MonoBehaviour,IEnemy,IDamageable
    {
        public void TakeDamage(int damage)
        {
            Debug.Log($"ダメージ{damage}を受けた");
        }
    }
}
