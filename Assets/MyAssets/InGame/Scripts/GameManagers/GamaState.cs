using System;
using R3;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// ゲームの状態を表す列挙
    /// </summary>
    public enum GameState
    {
        Init,
        Ready,
        Game,
        Adventure,
        Result
    }
    
    [Serializable]
    public class GameStateReactiveProperty : ReactiveProperty<GameState>
    {
        public GameStateReactiveProperty()
        {
        }

        public GameStateReactiveProperty(GameState initialValue)
            : base(initialValue)
        {
        }
    }
}
