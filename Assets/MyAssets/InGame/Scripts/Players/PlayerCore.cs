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
        
        private ReactiveProperty<bool> _isInitialize = new ReactiveProperty<bool>(false);
        public ReadOnlyReactiveProperty<bool> IsInitialize => _isInitialize;
        
        private PlayerParameter DefaultPLayerParameter = new PlayerParameter();

        private ReactiveProperty<PlayerParameter> _currentPlayerParameter =
            new ReactiveProperty<PlayerParameter>(new PlayerParameter());
        
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
    }
}
