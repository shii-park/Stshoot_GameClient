using System.Collections;
using StShoot.InGame.Players;
using StShoot.InGame.UIs;
using UnityEngine;

namespace StShoot
{
    public class Test4 : MonoBehaviour
    {
        [SerializeField]
        private PlayerBullet _bullet;
        
        [SerializeField]
        private CommentUIView _commentUIView;
        
        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }
        
        IEnumerator StartCoroutine()
        {
            while (true)
            {
                _bullet.AddReadyComments("あいうえお");
                _commentUIView.AddComment("俺","あいうえお");
                yield return new WaitForSeconds(0.5f);
                _bullet.AddReadyComments("かきくけこ");
                _commentUIView.AddComment("お前","かきくけこ");
                yield return new WaitForSeconds(0.5f);
                _bullet.AddReadyComments("さしすせそ");  
                _commentUIView.AddComment("名無しさん","さしすせそ");
                yield return new WaitForSeconds(0.5f);
                
            }
        }
    }
}
