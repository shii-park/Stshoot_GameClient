using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StShoot.InGame.Players.Inputs
{
    /// <summary>
    /// インゲームのデバッグ用入力クラス
    /// </summary>
    public class InGameInput : MonoBehaviour, IInGameInputEventProvider
    {
        private ReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>();
        private ReactiveProperty<bool> _onSpecialButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onPauseButton = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onSlowPushed = new ReactiveProperty<bool>();

        /// <summary>
        /// 移動方向
        /// </summary>
        public ReadOnlyReactiveProperty<Vector2> MoveDirection { get { return _moveDirection; } }
        /// <summary>
        /// スペシャルボタンが押されたか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> OnSpecialButtonPushed { get { return _onSpecialButtonPushed; } }
        /// <summary>
        /// ポーズボタンが押されたか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> OnPauseButton { get { return _onPauseButton; } }
        /// <summary>
        /// スローボタンが押されたか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> OnSlowPushed { get { return _onSlowPushed; } }
        
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
                .Subscribe(x => _onPauseButton.Value = x);
            
            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.leftShiftKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onSlowPushed.Value = x);
        }
    }
}
