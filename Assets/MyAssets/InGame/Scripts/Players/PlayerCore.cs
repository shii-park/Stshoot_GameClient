using System;
using R3;
using StShoot.InGame.GameManagers.Interfaces;
using StShoot.InGame.Players.Inputs;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public class PlayerCore : MonoBehaviour, IKillable
    {
        static private PlayerCore _player;

        static public PlayerCore Player
        {
            get
            {
                return _player;
            }
        }
        
        private PlayerCore()
        {
            _player = this;
        }
        
        private ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>(false);
        public ReadOnlyReactiveProperty<bool> IsDead => _isDead;
        
        private PlayerParameter DefaultPLayerParameter = new PlayerParameter();

        private ReactiveProperty<PlayerParameter> _currentPlayerParameter;
        
        private Transform _playerDefaultTransform;
        
        private const int DeathPenaltyPower = 10;
        
        /// <summary>
        /// 現在のSlimeのパラメータ
        /// </summary>
        public ReadOnlyReactiveProperty<PlayerParameter> CurrentPlayerParameter => _currentPlayerParameter;
        
        /// <summary>
        /// プレイヤーのパラメータを規定値に戻す
        /// </summary>
        public void ResetSlimeParameter()
        {
            _currentPlayerParameter.Value = DefaultPLayerParameter;
        }

        /// <summary>
        /// プレイヤーのパラメータを変更する
        /// </summary>
        public void SetSlimeParameter(PlayerParameter parameters)
        {
            _currentPlayerParameter.Value = parameters;
        }
        
        /// <summary>
        /// プレイヤーを即死させる
        /// </summary>
        public void Kill()
        {
            if(_isDead.Value) return;

            _currentPlayerParameter.Value.LifePoint--;
            if (_currentPlayerParameter.Value.LifePoint <= 0)
            {
                _currentPlayerParameter.Value.LifePoint = 0;
            }
            
            DecreasePower(DeathPenaltyPower);
            
            _isDead.Value = true;
        }
        
        public void IncreasePower(int amount)
        {
            _currentPlayerParameter.Value.PlayerPower += amount;
            if (_currentPlayerParameter.Value.PlayerPower >= _currentPlayerParameter.Value.MaxPlayerPower)
            {
                _currentPlayerParameter.Value.PlayerPower = _currentPlayerParameter.Value.MaxPlayerPower;
            }
        }
        
        public void DecreasePower(int amount)
        {
            _currentPlayerParameter.Value.PlayerPower -= amount;
            if (_currentPlayerParameter.Value.PlayerPower <= 0)
            {
                _currentPlayerParameter.Value.PlayerPower = 1;
            }
        }
        
        private ReactiveProperty<bool> _isInitialize = new ReactiveProperty<bool>(false);
        public ReadOnlyReactiveProperty<bool> IsInitialize => _isInitialize;

        public void Initialize(PlayerParameter initialParameter, Transform playerDefaultTransform)
        {
            _playerDefaultTransform = playerDefaultTransform;
            
            DefaultPLayerParameter = initialParameter;
            
            _isInitialize.OnNext(true);
            _isInitialize.OnCompleted();
            
            _currentPlayerParameter = new ReactiveProperty<PlayerParameter>(DefaultPLayerParameter);
            
            _isDead
                .Where(_ => _isDead.Value)
                .Subscribe(_ =>
                {
                    if (_currentPlayerParameter.Value.LifePoint <= -1)
                    {
                        Debug.Log("Game Over");
                        return;
                    }
                    //1秒後に元の状態に
                    Observable.Timer(TimeSpan.FromSeconds(1))
                        .Subscribe(_ =>
                        {
                            transform.position = _playerDefaultTransform.position;
                            _isDead.Value = false;
                        });
                });
        }
    }
}
