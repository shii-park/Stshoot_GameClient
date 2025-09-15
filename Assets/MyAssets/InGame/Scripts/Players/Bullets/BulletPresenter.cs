using UnityEngine;
using R3;

namespace StShoot.InGame.Players.Bullets
{
    /// <summary>
    /// プレイヤーの弾のプレゼンター
    /// </summary>
    public class BulletPresenter : MonoBehaviour
    {
        public BulletModel Model = new BulletModel();

        [SerializeField]
        private BulletView _view;

        public void  Start()
        {
            // Modelのテキストの変化をViewに反映させる
            Model.CommentChar.Subscribe(text => 
                _view.SetText(text)
            );
            
            // Modelの利用可能状態の変化をViewに反映させる
            Model.IsAvailable.Subscribe(isAvailable => 
                _view.SetActive(isAvailable == false)
            );
        }
    }
}