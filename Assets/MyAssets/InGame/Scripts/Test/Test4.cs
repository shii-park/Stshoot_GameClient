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
            Debug.Log("Start");
            while (true)
            {
                _bullet.AddReadyComments("あいうえお");
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
