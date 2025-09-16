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
        
        public BaseEnemy Model => _model;
        
        private void Start()
        {
            _model.IsAlive.Subscribe(isAlive =>
            {
                _view.SetActive(isAlive);
            });
        }
    }
}
