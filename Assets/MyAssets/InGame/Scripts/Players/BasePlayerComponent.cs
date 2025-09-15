using R3;
using StShoot.InGame.Players.Inputs;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public abstract class BasePlayerComponent : MonoBehaviour
    {
        private IInGameInputEventProvider _inGameInputEventProvider;
        
        /// <summary>
        /// 入力のイベント通知
        /// </summary>
        protected IInGameInputEventProvider InGameInputEventProvider { get { return _inGameInputEventProvider; } }

        /// <summary>
        /// プレイヤーの基本的な情報
        /// </summary>
        protected PlayerCore PlayerCore;
        
        /// <summary>
        /// 現在のプレイヤーのパラメータ
        /// </summary>
        protected ReadOnlyReactiveProperty<PlayerParameter> CurrentSlimeParameter
        {
            get
            {
                return PlayerCore.CurrentPlayerParameter;
            }
        }
        
        private void Start()
        {
            PlayerCore = GetComponent<PlayerCore>();
            _inGameInputEventProvider = GetComponent<IInGameInputEventProvider>();

            //Coreの情報が確定したら初期化を呼び出す
            PlayerCore.IsInitialize
                .Skip(1)
                .Subscribe(_ => OnInitialize());

            OnStart();
        }

        /// <summary>
        /// Start() と同じタイミング
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// プレイヤ情報の初期化が完了した時に実行される初期化処理
        /// </summary>
        protected abstract void OnInitialize();
    }
}
