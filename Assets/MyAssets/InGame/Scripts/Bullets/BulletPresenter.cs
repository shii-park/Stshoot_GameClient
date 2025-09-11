using UnityEngine;
using R3;

namespace Assets.MyAssets.InGame.Bullets
{
    public class BulletPresenter : MonoBehaviour
    {
        public BulletModel Model = new BulletModel();

        [SerializeField]
        private BulletView _view;

        public void  Start()
        {
            Model.CommentChar.Subscribe(text => 
                _view.SetText(text)
            );

            Model.IsAvailable.Subscribe(isAvailable => 
                _view.SetActive(isAvailable == false)
            );
        }
    }
}