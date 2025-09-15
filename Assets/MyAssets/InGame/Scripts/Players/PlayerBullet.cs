using System.Collections;
using System.Collections.Generic;
using R3;
using StShoot.InGame.Players.Bullets;
using UnityEngine;

namespace StShoot.InGame.Players
{
    /// <summary>
    /// プレイヤーの弾を制御するクラス
    /// </summary>
    public class PlayerBullet : BasePlayerComponent
    {
        private readonly List<string> _readyComments = new List<string>();
        private readonly ReactiveProperty<int> _commentCount = new ReactiveProperty<int>(0);
        
        private List<GameObject> _bulletGameObjects = new List<GameObject>();

        public ReadOnlyReactiveProperty<int> CommentCount => _commentCount;
        
        private Coroutine _shotCharacterCoroutine;
        
        [SerializeField]
        private GameObject _bulletGameObject;
        
        private float _shotInterval = 0.05f;
        
        protected override void OnInitialize()
        {
            ClearReadyComments();
            _shotCharacterCoroutine = null;
        }

        protected override void OnStart()
        {
            CommentCount
                .Where(count => count >= 0)
                .Subscribe(_ =>
                {
                    ShotComment();
                });
        }

        private void ShotComment()
        {
            if (_shotCharacterCoroutine != null || _readyComments.Count <= 0) return;
            
            _shotCharacterCoroutine = StartCoroutine(ShotCharacterCoroutine(_readyComments[0]));
            
        }
        
        private IEnumerator ShotCharacterCoroutine(string comment)
        {
            int count = 0;
            
            while (count < comment.Length)
            {
                Vector3 vec = PlayerCore.Player.gameObject.transform.position;
                
                GameObject instance = null;


                foreach (var _bullet in _bulletGameObjects)
                {
                    if (_bullet == null) continue;
                    var pre = _bullet.GetComponent<BulletPresenter>();
                    if (pre.Model.IsAvailable.CurrentValue)
                    {
                        instance = _bullet;
                        instance.transform.position = vec;
                        pre.Model.SetAvailable(false);
                        break;
                    }
                }
                if(instance == null)
                {
                    instance = Instantiate(_bulletGameObject, vec, Quaternion.identity);
                    _bulletGameObjects.Add(instance);
                }
                
                var instansPre = instance.GetComponent<BulletPresenter>();
                var move = instance.GetComponent<BulletMove>();
                instansPre.Model.SetAvailable(false);
                instansPre.Model.SetCommentChar(comment[count].ToString());

                move.MoveBullet();
                count++;
                yield return new WaitForSeconds(_shotInterval);
            } ;
            RemoveReadyCommentsFirst();
            _shotCharacterCoroutine = null;
        }
        
        public void AddReadyComments(string item)
        {
            _readyComments.Add(item);
            _commentCount.Value = _readyComments.Count;
        }

        public void RemoveReadyCommentsFirst()
        {
            if (_readyComments.Count == 0) return;

            _readyComments.RemoveAt(0);
            _commentCount.Value = _readyComments.Count;
        }
        
        public void ClearReadyComments()
        {
            _readyComments.Clear();
            _commentCount.Value = 0;
        }
        
        public void ClearAllBullets()
        {
            foreach (var bullet in _bulletGameObjects)
            {
                Destroy(bullet);
            }
            _bulletGameObjects.Clear();
        }
    }
}
