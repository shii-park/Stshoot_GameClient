using UnityEngine;
using System.Collections;


namespace StShoot.InGame.Scripts.Players.Bullets
{
    public class BulletMove : MonoBehaviour
    {
        private bool _isMoving;

        [SerializeField]
        private float _bulletSpeed;

        public void MoveBullet(){
            _isMoving = true;
            StartCoroutine(MoveBulletCoroutine());
        }

        public void StopBullet(){
            _isMoving = false;
        }

        IEnumerator MoveBulletCoroutine()
        {
            // 条件にゲームの状態を増やす
            while (_isMoving)
            {
                var pos = this.gameObject.transform.position;
                pos.y += 0.1f * _bulletSpeed;
                this.gameObject.transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}