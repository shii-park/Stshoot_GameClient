using System.Collections;
using StShoot.InGame.Players;
using UnityEngine;

namespace StShoot
{
    public class Test6 : MonoBehaviour
    {
        [SerializeField] private PlayerCore _playerCore;
        
        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }
        
        IEnumerator StartCoroutine()
        {
            while (true)
            {
                _playerCore.IncreasePower(1);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
