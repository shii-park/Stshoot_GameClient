using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.UIs
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager _model;

        [SerializeField]
        private ScoreView _view;

        private void Start()
        {
            _model.CurrentScore.Subscribe(score =>
            {
                _view.SetCurrentScore(score);
            });
        }
    }
}
