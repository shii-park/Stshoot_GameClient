using System.Collections;
using System.Collections.Generic;
using StShoot.InGame.Enemies;
using UnityEngine;
using EnemyFactory = StShoot.InGame.Enemies.EnemyFactory;

namespace StShoot
{
    public class Test9 : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemies;
        
        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }
        
        IEnumerator StartCoroutine()
        {
            while (true)
            {
                EnemyFactory.Instance.Create(
                    _enemies[0].name, 
                    new Vector3(-4.5f, 3.6f, 0f), new List<Waypoint>
                {
                    new Waypoint(new Vector3(4.5f, 3.6f, 0f), 7f, MoveType.Straight),
                });
                
                EnemyFactory.Instance.Create(
                    _enemies[1].name, 
                    new Vector3(4.5f, 1.8f, 0f), new List<Waypoint>
                    {
                        new Waypoint(new Vector3(-4.5f, 1.8f, 0f), 7f, MoveType.Straight),
                    });
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
