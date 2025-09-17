using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StShoot.OutGame.Inputs
{
    public class OutGameInput : MonoBehaviour,IOutGameInputEventProvider
    {
        private ReactiveProperty<bool> _onUpButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onDownButtonPushed = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _onDecideButtonPushed = new ReactiveProperty<bool>();
        
        public ReadOnlyReactiveProperty<bool> UpButtonPushed => _onUpButtonPushed;
        public ReadOnlyReactiveProperty<bool> DownButtonPushed => _onDownButtonPushed;
        public ReadOnlyReactiveProperty<bool> OnDecideButtonPushed => _onDecideButtonPushed;    
        
        void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.upArrowKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onUpButtonPushed.Value = x);

            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.downArrowKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onDownButtonPushed.Value = x);
            
            this.UpdateAsObservable()
                .Select(_ => Keyboard.current.spaceKey.isPressed)
                .DistinctUntilChanged()
                .Subscribe(x => _onDecideButtonPushed.Value = x);
        }
    }
}
