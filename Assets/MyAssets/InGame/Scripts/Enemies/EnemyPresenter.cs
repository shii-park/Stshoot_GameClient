using UnityEngine;
using R3;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// エネミーのプレゼンタークラス
    /// </summary>
    public class EnemyPresenter : MonoBehaviour
    {
        [SerializeField]
        private EnemyView _view;
        
        [SerializeField]
        private BaseEnemy _model;
        
        private void Start()
        {
            _model.IsAlive.Subscribe(isAvailable =>
            {
                _view.SetActive(isAvailable == false);
            });
        }
    }
}
