using R3;

namespace StShoot.OutGame.Inputs
{
    public interface IOutGameInputEventProvider
    {
        ReadOnlyReactiveProperty<bool> UpButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> DownButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> OnDecideButtonPushed { get; }
    }
}
