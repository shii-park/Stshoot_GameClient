using System.Collections;
using UnityEngine;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテムの動きを管理するクラス
    /// </summary>
    public class ItemMove : MonoBehaviour
    {
        private float initialUpTime = 0.2f;
        private float upSpeed = 1.0f;
        private float gravity = 4.9f;
        private float maxFallSpeed = 10.0f;
        
        private bool _isMoving;
        
        [SerializeField]
        private ItemPresenter _itemPresenter;

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public void Init()
        {
            StartCoroutine(FloatAndFallCoroutine());
        }
        
        /// <summary>
        /// 弾を止めるメソッド
        /// </summary>
        public void StopBullet(){
            _isMoving = false;
        }

        /// <summary>
        /// 弾を動かすコルーチン
        /// </summary>
        private IEnumerator FloatAndFallCoroutine()
        {
            // 一瞬上昇
            float timer = 0f;
            while (_isMoving == false && timer < initialUpTime)
            {
                transform.position += Vector3.up * upSpeed * 0.01f;
                timer += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }

            // ふんわり落下（だんだん加速）
            float fallSpeed = 0f;
            while (_isMoving == false)
            {
                fallSpeed += gravity * 0.01f;
                fallSpeed = Mathf.Min(fallSpeed, maxFallSpeed);
                transform.position -= Vector3.up * fallSpeed * 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            
            _itemPresenter.Model.SetAvailable(true);
        }
    }
}