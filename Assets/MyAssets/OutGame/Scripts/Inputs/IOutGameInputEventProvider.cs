using R3;

namespace StShoot.OutGame.Inputs
{
    public class IOutGameInputEventProvider
    {
        ReadOnlyReactiveProperty<bool> UpButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> DownButtonPushed { get; }
        ReadOnlyReactiveProperty<bool> OnDecideButtonPushed { get; }
    }
}
