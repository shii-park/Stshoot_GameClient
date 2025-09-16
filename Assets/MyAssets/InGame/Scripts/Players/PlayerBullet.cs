using System.Collections;
using System.Collections.Generic;
using R3;
using StShoot.InGame.GameManagers;
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

        /// <summary>
        /// 準備中のコメント数
        /// </summary>
        public ReadOnlyReactiveProperty<int> CommentCount => _commentCount;
        
        private Coroutine _shotCharacterCoroutine;
        
        [SerializeField]
        private GameObject _bulletGameObject;
        
        private const float ShotInterval = 0.07f;
        
        [SerializeField]
        private List<Transform> _generatePositions = new List<Transform>();
        
        private GameObject _bulletsParent;
        
        protected override void OnInitialize()
        {
            ClearReadyComments();
            _shotCharacterCoroutine = null;
            
            // 管理用の親オブジェクトを生成（なければ）
            if (_bulletsParent == null)
            {
                _bulletsParent = new GameObject("PlayerBulletsParent");
            }
            
            CommentCount
                .Where(count => count >= 0)
                .Subscribe(_ =>
                {
                    ShotComment();
                });
        }

        protected override void OnStart()
        {

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
            int count = 0;
            
            while (count < comment.Length)
            {
                if (PlayerCore.IsDead.CurrentValue || MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game)
                {
                    yield return new WaitUntil(() => PlayerCore.IsDead.CurrentValue == false && MainGameManager.Instance.CurrentGameState.CurrentValue == GameState.Game);
                }

                for (int i = 0; i < 5; i++)
                {
                    if (GetGenerateNumber()[i] == '1')
                    {
                        GenerateBullet(comment[count].ToString(), GetGenerateAngle(i), i);
                    }
                }

                count++;
                yield return new WaitForSeconds(ShotInterval);
            } ;
            RemoveReadyCommentsFirst();
            _shotCharacterCoroutine = null;
        }
        
        /// <summary>
        /// 弾を生成するメソッド
        /// </summary>
        /// <param name="commentChar">弾の文字</param>
        /// <param name="angleDeg">角度</param>
        /// <param name="index">生成する位置</param>
        private void GenerateBullet(string commentChar, float angleDeg, int index)
        {
            Vector3 vec = _generatePositions[index].position;
            GameObject instance = null;

            foreach (var _bullet in _bulletGameObjects)
            {
                if (_bullet == null) continue;
                var pre = _bullet.GetComponent<BulletPresenter>();
                if (pre.Model.IsAvailable.CurrentValue)
                {
                    instance = _bullet;
                    instance.transform.SetParent(_bulletsParent.transform, false);
                    instance.transform.position = vec;
                    pre.Model.SetAvailable(false);
                    break;
                }
            }
            if (instance == null)
            {
                instance = Instantiate(_bulletGameObject, vec, Quaternion.identity, _bulletsParent.transform);
                _bulletGameObjects.Add(instance);
            }

            var instansPre = instance.GetComponent<BulletPresenter>();
            var move = instance.GetComponent<BulletMove>();
            instansPre.Model.SetAvailable(false);
            instansPre.Model.SetCommentChar(commentChar);

            move.MoveBullet(angleDeg);
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
        
        /// <summary>
        /// 全ての弾を削除するメソッド
        /// </summary>
        public void ClearAllBullets()
        {
            foreach (var bullet in _bulletGameObjects)
            {
                Destroy(bullet);
            }
            _bulletGameObjects.Clear();
        }
        
        /// <summary>
        /// プレイヤーのパワーに応じて生成する弾の位地を決定するメソッド
        /// </summary>
        private string GetGenerateNumber()
        {
            switch (PlayerCore.CurrentPlayerParameter.CurrentValue.PlayerPower)
            {
                case int n when (n >= 1 && n <= 8): 
                    return "10000"; 
                case int n when (n >= 9 && n <= 32): 
                    return "11100";
                case int n when (n >= 33 && n <= 64): 
                    return "10011";
                case int n when (n >= 65 && n <= 128):
                    return "11111";
                default:
                    return "00000";
            }
        }
        
        /// <summary>
        /// 生成する弾の角度を決定するメソッド
        /// </summary>
        /// <param name="index">生成位置</param>
        private float GetGenerateAngle(int index)
        {
            switch (index)
            {
                case 0:
                case 3:    
                case 4:
                    return 90;
                case 1:
                    return 50f;
                case 2:
                    return 130f;
                default:
                    return 0;
            }
        }
    }
}
