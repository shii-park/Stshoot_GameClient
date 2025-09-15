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
        
        private ReactiveProperty<bool> _isInitialize = new ReactiveProperty<bool>(false);
        public ReadOnlyReactiveProperty<bool> IsInitialize { get { return _isInitialize; } }
    }
}
