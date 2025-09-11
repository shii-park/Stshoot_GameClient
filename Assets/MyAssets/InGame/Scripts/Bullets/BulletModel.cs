using UnityEngine;
using R3;

public class BulletModel
{
    private ReactiveProperty<string> _commentChar = new ReactiveProperty<string>();
    public ReadOnlyReactiveProperty<string> CommentChar { get { return _commentChar; } }

    private ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);
    public ReadOnlyReactiveProperty<bool> IsAvailable { get { return _isAvailable; } }

    public int BulletPower = 1;

    public BulletModel(){
        SetAvailabl(true);
    }

    public void SetCommentChar(string newCommentChar){
        _commentChar.Value = newCommentChar;
    }

    public void SetAvailabl(bool isAvailable){
        _isAvailable.Value = isAvailable;
    }
}
