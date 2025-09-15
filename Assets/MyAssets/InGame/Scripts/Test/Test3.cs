using System.Collections;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot
{
    public class Test3 : MonoBehaviour
    {
        [SerializeField]ScoreManager scoreManager;
        
        IEnumerator Start()
        {
            while (true)
            {
                scoreManager.AddScore(100000);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
