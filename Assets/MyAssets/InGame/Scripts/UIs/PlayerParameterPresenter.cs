using R3;
using StShoot.InGame.Players;
using UnityEngine;

namespace StShoot.InGame.UIs
{
    public class PlayerParameterPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerCore _model;

        [SerializeField]
        private PlayerParameterView _view;

        private void Start()
        {
            _model.CurrentPower.Subscribe(_ =>
            {
                _view.SetPlayerPower(_model.CurrentPower.Value, _model.CurrentPlayerParameter.CurrentValue.MaxPlayerPower);
            });
            
            _model.CurrentLifePoint.Subscribe(_ =>
            {
                _view.SetPlayerLifePoint(_model.CurrentLifePoint.Value);
            });
        }
    }
}
