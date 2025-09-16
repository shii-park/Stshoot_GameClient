using UnityEngine;
using R3;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテムのプレゼンタークラス
    /// </summary>
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private BaseItem _model;
        public BaseItem Model => _model;

        private void Start()
        {
            _model.IsAvailable.Subscribe(isAvailable =>
            {
                _view.SetActive(isAvailable == false);
            });
        }
    }
}
