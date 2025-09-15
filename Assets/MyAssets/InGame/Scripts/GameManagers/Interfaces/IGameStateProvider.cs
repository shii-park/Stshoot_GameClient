using R3;

namespace StShoot.InGame.GameManagers.Interfaces
{
    public interface IGameStateProvider
    {
        ReadOnlyReactiveProperty<GameState> CurrentGameState { get; }
    }
}
