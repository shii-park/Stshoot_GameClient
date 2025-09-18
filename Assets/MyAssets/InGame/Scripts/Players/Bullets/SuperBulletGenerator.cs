using System.Collections;
using System.Collections.Generic;
using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Players.Bullets
{
    public class SuperBulletGenerator : MonoBehaviour
    {
        private readonly List<string> _readyComments = new List<string>();
        private readonly ReactiveProperty<int> _commentCount = new ReactiveProperty<int>(0);
        
        /// <summary>
        /// 準備中のコメント数
        /// </summary>
        public ReadOnlyReactiveProperty<int> CommentCount => _commentCount;
        
        private Coroutine _shotCharacterCoroutine;
        
        [SerializeField]
        private GameObject _superBulletGameObject;
        
        private GameObject _superBulletInstance;
        
        private const float ShotInterval = 20f;
        
        private PlayerCore _playerCore;
        
        private SuperBullet _superBullet;
        
        void OnInitialize()
        {
            _shotCharacterCoroutine = null;
            _playerCore = GetComponent<PlayerCore>();
            
            CommentCount
                .Where(count => count >= 0)
                .ObserveOnMainThread()
                .Subscribe(_ =>
                {
                    ShotComment();
                });

            
            _playerCore.IsGameOver
                .Where(isGameOver => isGameOver)
                .Subscribe(_ =>
                {
                    ClearReadyComments();
                    Destroy(_superBulletInstance);
                });
        }


        /// <summary>
        /// コメントの文字を撃つメソッド
        /// </summary>
        private void ShotComment()
        {
            if (_shotCharacterCoroutine != null || _readyComments.Count <= 0) return;
            
            _shotCharacterCoroutine = StartCoroutine(ShotCharacterCoroutine(_readyComments[0]));
        }
        
        /// <summary>
        /// コメントの文字を撃つコルーチン
        /// </summary>
        /// <param name="comment">コメントの文字</param>
        private IEnumerator ShotCharacterCoroutine(string comment)
        {
            if (_playerCore.IsDead.CurrentValue || MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game || _superBullet.IsAvailable == false)
            {
                yield return new WaitUntil(() => _playerCore.IsDead.CurrentValue == false && MainGameManager.Instance.CurrentGameState.CurrentValue == GameState.Game && _superBullet.IsAvailable);
            }
            
            GenerateBullet(_readyComments[0]);
            
            yield return new WaitForSeconds(ShotInterval);
            
            RemoveReadyCommentsFirst();
            _shotCharacterCoroutine = null;
            
        }
        
        /// <summary>
        /// 弾を生成するメソッド
        /// </summary>
        /// <param name="commentChar">弾の文字</param>
        /// <param name="angleDeg">角度</param>
        /// <param name="index">生成する位置</param>
        private void GenerateBullet(string comment)
        {
            if (_superBulletInstance == null)
            {
                _superBulletInstance = Instantiate(_superBulletGameObject, transform.position, Quaternion.identity, transform);
                _superBullet = _superBulletInstance.GetComponent<SuperBullet>();
                _superBullet.Set(comment);
            }
            else
            {
                _superBullet.Set(comment);
                _superBulletInstance.transform.position = transform.position;
                _superBulletInstance.SetActive(true);
            }

            var move = _superBulletInstance.GetComponent<BulletMove>();
            move.MoveBullet(90f);
        }
        
        /// <summary>
        /// 準備中のコメントを追加するメソッド
        /// </summary>
        /// <param name="item">追加するコメント</param>
        public void AddReadyComments(string item)
        {
            _readyComments.Add(item);
            _commentCount.Value = _readyComments.Count;
        }

        /// <summary>
        /// 準備中のコメントの先頭を削除するメソッド
        /// </summary>
        public void RemoveReadyCommentsFirst()
        {
            if (_readyComments.Count == 0) return;

            _readyComments.RemoveAt(0);
            _commentCount.Value = _readyComments.Count;
        }
        
        /// <summary>
        /// 準備中のコメントを全て削除するメソッド
        /// </summary>
        public void ClearReadyComments()
        {
            _readyComments.Clear();
            _commentCount.Value = 0;
        }
    }
}
