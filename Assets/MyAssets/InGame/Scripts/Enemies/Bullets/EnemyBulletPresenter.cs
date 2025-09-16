using UnityEngine;
using R3;

namespace StShoot.InGame.Enemies.Bullets
{
    public class EnemyBulletPresenter : MonoBehaviour
    {
        [SerializeField]
        private BaseEnemyBullet _model;
        [SerializeField]
        private EnemyBulletView _view;

        private void Start()
        {
            _model.IsAvailable.Subscribe(isAvailable =>
            {
                _view.SetActive(isAvailable == false);
            });
        }
    }
}
