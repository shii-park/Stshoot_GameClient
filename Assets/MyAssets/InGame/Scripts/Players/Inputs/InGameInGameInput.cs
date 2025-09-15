using System;
using R3;
using R3.Triggers;
using UnityEngine;

namespace MyAssets.InGame.Players.Inputs
{
    public class InGameDebugInput : MonoBehaviour, IInGameInputEventProvider
    {
        private ReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>();
        private ReactiveProperty<bool> _onSpecialButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _pauseButton = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<Vector2> MoveDirection { get { return _moveDirection; } }
        public ReadOnlyReactiveProperty<bool> OnSpecialButtonPushed { get { return _onSpecialButtonPushed; } }
        public ReadOnlyReactiveProperty<bool> PauseButton { get { return _pauseButton; } }
        
        void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => _moveDirection.OnNext(new Vector2(
                    Convert.ToInt32(Input.GetKey(KeyCode.RightArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow)), 
                    Convert.ToInt32(Input.GetKey(KeyCode.UpArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.DownArrow))
                )));

            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Space))
                .DistinctUntilChanged()
                .Subscribe(x => _onSpecialButtonPushed.Value = x);
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Escape))
                .DistinctUntilChanged()
                .Subscribe(x => _pauseButton.Value = x);
        }
    }
}
