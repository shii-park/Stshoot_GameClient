using System.Collections;
using StShoot.InGame.Enemies;
using UnityEngine;

namespace StShoot
{
    public class Test9 : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemy;
        
        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }
        
        IEnumerator StartCoroutine()
        {
            while (true)
            {
                Instantiate(_enemy, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2f, 5f), 0), Quaternion.identity).GetComponent<BaseEnemy>().Init();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
