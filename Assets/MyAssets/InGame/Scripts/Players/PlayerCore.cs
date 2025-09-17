using System;
using R3;
using StShoot.InGame.GameManagers;
using StShoot.InGame.GameManagers.Interfaces;
using StShoot.InGame.Players.Inputs;
using UnityEngine;

namespace StShoot.InGame.Players
{
    /// <summary>
    /// プレイヤーのコアクラス
    /// </summary>
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
        /// <summary>
        /// プレイヤーが死亡しているか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsDead => _isDead;
        
        private ReactiveProperty<bool> _isGameOver = new ReactiveProperty<bool>(false);
        /// <summary>
        /// ゲームオーバーか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsGameOver => _isGameOver;
        
        private PlayerParameter DefaultPLayerParameter = new PlayerParameter();

        private ReactiveProperty<PlayerParameter> _currentPlayerParameter;
        
        private ReactiveProperty<int> _currentLifePoint = new ReactiveProperty<int>();
        public ReactiveProperty<int> CurrentLifePoint => _currentLifePoint;
        
        private ReactiveProperty<int> _currentPower = new ReactiveProperty<int>();
        public ReactiveProperty<int> CurrentPower => _currentPower;
        
        private Transform _playerDefaultTransform;
        
        private const int DeathPenaltyPower = 10;
        
        /// <summary>
        /// 現在のSlimeのパラメータ
        /// </summary>
        public ReadOnlyReactiveProperty<PlayerParameter> CurrentPlayerParameter => _currentPlayerParameter;

        private void Start()
        {
            if (MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Init)
            {
                MainGameManager.Instance.SetGameState(GameState.Init);
            }
        }
        
        /// <summary>
        /// プレイヤーのパラメータを規定値に戻すメソッド
        /// </summary>
        public void ResetSlimeParameter()
        {
            _currentPlayerParameter.Value = DefaultPLayerParameter;
            _currentPower.Value = DefaultPLayerParameter.PlayerPower;
            _currentLifePoint.Value = DefaultPLayerParameter.LifePoint;
        }

        /// <summary>
        /// プレイヤーのパラメータを変更するメソッド
        /// </summary>
        public void SetSlimeParameter(PlayerParameter parameters)
        {
            _currentPlayerParameter.Value = parameters;
            _currentPower.Value = parameters.PlayerPower;
            _currentLifePoint.Value = parameters.LifePoint;
        }
        
        /// <summary>
        /// プレイヤーを即死させるメソッド
        /// </summary>
        public void Kill()
        {
            if(_isDead.Value) return;

            _currentPlayerParameter.Value.LifePoint--;
            _currentLifePoint.Value = _currentPlayerParameter.Value.LifePoint;
            
            DecreasePower(DeathPenaltyPower);
            
            _isDead.Value = true;
        }
        
        /// <summary>
        /// プレイヤーのパワーを増加させるメソッド
        /// </summary>
        /// <param name="amount">増加量</param>
        public void IncreasePower(int amount)
        {
            _currentPlayerParameter.Value.PlayerPower += amount;
            _currentPower.Value = _currentPlayerParameter.Value.PlayerPower;
            
            if (_currentPlayerParameter.Value.PlayerPower >= _currentPlayerParameter.Value.MaxPlayerPower)
            {
                _currentPlayerParameter.Value.PlayerPower = _currentPlayerParameter.Value.MaxPlayerPower;
                _currentPower.Value = _currentPlayerParameter.Value.MaxPlayerPower;
            }
        }
        
        /// <summary>
        /// プレイヤーのパワーを減少させるメソッド
        /// </summary>
        /// <param name="amount">減少量</param>
        public void DecreasePower(int amount)
        {
            _currentPlayerParameter.Value.PlayerPower -= amount;
            _currentPower.Value = _currentPlayerParameter.Value.PlayerPower;
            
            if (_currentPlayerParameter.Value.PlayerPower <= 0)
            {
                _currentPlayerParameter.Value.PlayerPower = 1;
                _currentPower.Value = 1;
            }
        }
        
        private ReactiveProperty<bool> _isInitialize = new ReactiveProperty<bool>(false);
        /// <summary>
        /// プレイヤーの初期化が完了したか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsInitialize => _isInitialize;

        /// <summary>
        /// プレイヤーの初期化を行うメソッド
        /// </summary>
        /// <param name="initialParameter">初期パラメータ</param>
        /// <param name="playerDefaultTransform">初期位置</param>
        public void Initialize(PlayerParameter initialParameter, Transform playerDefaultTransform)
        {
            _playerDefaultTransform = playerDefaultTransform;
            
            DefaultPLayerParameter = initialParameter;
            
            _isInitialize.Value = true;
            _isInitialize.OnCompleted();
            
            _currentPlayerParameter = new ReactiveProperty<PlayerParameter>(DefaultPLayerParameter);
            
            _currentLifePoint.Value = _currentPlayerParameter.Value.LifePoint;
            _currentPower.Value = _currentPlayerParameter.Value.PlayerPower;
            
            _isDead
                .Where(_ => _isDead.Value)
                .Subscribe(_ =>
                {
                    if (_currentPlayerParameter.Value.LifePoint <= -1)
                    {
                        _isGameOver.Value = true;
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
