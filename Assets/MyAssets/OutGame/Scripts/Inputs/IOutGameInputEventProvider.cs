using R3;

namespace StShoot.OutGame.Inputs
{
    public interface IOutGameInputEventProvider
    {
        ReadOnlyReactiveProperty<bool> LeftButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> RightButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> OnDecideButtonPushed { get; }
    }
}
