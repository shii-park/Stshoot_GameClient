using UnityEngine;
using R3;

namespace StShoot.InGame.Items
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private BaseItem _model;
        public BaseItem Model => _model;

        private void Start()
        {
            _model.IsAvailable.Subscribe(isAlive =>
            {
                _view.SetActive(isAlive);
            });
        }
    }
}
