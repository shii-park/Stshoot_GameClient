using R3;

namespace StShoot.InGame.Scripts.Players.Bullets
{
    /// <summary>
    /// プレイヤーの弾のモデル
    /// </summary>
    public class BulletModel
    {
        private ReactiveProperty<string> _commentChar = new ReactiveProperty<string>();
        /// <summary>
        /// コメントの文字のプロパティ
        /// </summary>
        public ReadOnlyReactiveProperty<string> CommentChar { get { return _commentChar; } }

        private ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);
        /// <summary>
        /// 利用可能状態のプロパティ
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsAvailable { get { return _isAvailable; } }

        /// <summary>
        /// 弾の威力
        /// </summary>
        public int BulletPower = 1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BulletModel(){
            SetAvailable(true);
        }

        /// <summary>
        /// 弾の文字を設定するメソッド
        /// </summary>
        /// <param name="newCommentChar">文字は一文字</param>
        public void SetCommentChar(string newCommentChar){
            _commentChar.Value = newCommentChar;
        }

        /// <summary>
        /// 弾の利用可能状態を設定するメソッド
        /// </summary>
        /// <param name="isAvailable">Trueだったら利用可能、Falseだったら利用不可</param>
        public void SetAvailable(bool isAvailable){
            _isAvailable.Value = isAvailable;
        }
    }
}