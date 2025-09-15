using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StShoot.InGame.Players.Inputs
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
                    Convert.ToInt32(Keyboard.current.rightArrowKey.isPressed) - Convert.ToInt32(Keyboard.current.leftArrowKey.isPressed),
                    Convert.ToInt32(Keyboard.current.upArrowKey.isPressed) - Convert.ToInt32(Keyboard.current.downArrowKey.isPressed)
                )));

            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.spaceKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onSpecialButtonPushed.Value = x);

            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.escapeKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _pauseButton.Value = x);
        }
    }
}
