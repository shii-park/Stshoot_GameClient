namespace StShoot.InGame.Common.Interfaces
{
    /// <summary>
    /// ダメージを受けることができるオブジェクトのインターフェース
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// ダメージを受け取るメソッド
        /// </summary>
        /// <param name="damage">ダメージの値</param>
        void TakeDamage(int damage);
    }
}
