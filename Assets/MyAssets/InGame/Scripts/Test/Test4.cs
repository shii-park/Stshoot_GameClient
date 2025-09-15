using System.Collections;
using StShoot.InGame.Players;
using UnityEngine;

namespace StShoot
{
    public class Test4 : MonoBehaviour
    {
        [SerializeField]
        private PlayerBullet _bullet;
        
        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }
        
        IEnumerator StartCoroutine()
        {
            while (true)
            {
                _bullet.AddReadyComments("あいうえお");
                _bullet.AddReadyComments("かきくけこ");
                _bullet.AddReadyComments("さしすせそ");
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
