using System.Collections;
using System.Collections.Generic;
using R3;
using StShoot.InGame.Players.Bullets;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public class PlayerBullet : BasePlayerComponent
    {
        private readonly List<string> _readyComments = new List<string>();
        private readonly ReactiveProperty<int> _commentCount = new ReactiveProperty<int>(0);

        public ReadOnlyReactiveProperty<int> CommentCount => _commentCount;
        
        private Coroutine _shotCharacterCoroutine;
        
        [SerializeField]
        private GameObject _bulletGameObject;
        
        protected override void OnInitialize()
        {
            ClearReadyComments();
            _shotCharacterCoroutine = null;
        }

        protected override void OnStart()
        {
            _readyComments.Clear();
            _commentCount.Value = 0;
            
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
                var prefab = Instantiate(_bulletGameObject,vec,Quaternion.identity);
                var pre = prefab.GetComponent<BulletPresenter>();
                var move = prefab.GetComponent<BulletMove>();
                pre.Model.SetAvailable(false);
                pre.Model.SetCommentChar(comment[count].ToString());

                move.MoveBullet();
                count++;
                yield return new WaitForSeconds(0.05f);
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
    }
}
