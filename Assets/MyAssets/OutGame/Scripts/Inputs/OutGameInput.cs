using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StShoot.OutGame.Inputs
{
    public class OutGameInput : MonoBehaviour,IOutGameInputEventProvider
    {
        private ReactiveProperty<bool> _onLeftButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onRightButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onDecideButtonPushed = new ReactiveProperty<bool>();
        
        public ReadOnlyReactiveProperty<bool> LeftButtonPushed => _onLeftButtonPushed;
        public ReadOnlyReactiveProperty<bool> RightButtonPushed => _onRightButtonPushed;
        public ReadOnlyReactiveProperty<bool> OnDecideButtonPushed => _onDecideButtonPushed;    
        
        void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.leftArrowKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onLeftButtonPushed.Value = x);

            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.rightArrowKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onRightButtonPushed.Value = x);
            
            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.spaceKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onDecideButtonPushed.Value = x);
        }
    }
}
