using R3;
using StShoot.InGame.Players.Inputs;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public class PlayerCore : MonoBehaviour
    {
        static private PlayerCore player;

        static public PlayerCore Player
        {
            get
            {
                return player;
            }
        }
        
        private PlayerCore()
        {
            player = this;
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
            
            _currentPlayerParameter.Value.PlayerPower -= DeathPenaltyPower;
            if (_currentPlayerParameter.Value.PlayerPower <= 0) _currentPlayerParameter.Value.PlayerPower = 1;
            
            _isDead.Value = true;
        }
    }
}
