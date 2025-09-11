using UnityEngine;
using R3;

public class BulletModel
{
    private ReactiveProperty<char> _commentChar = new ReactiveProperty<char>();
    public ReadOnlyReactiveProperty<char> CommentChar { get { return _commentChar; } }

    private ReactiveProperty<bool> _isAvailable = new ReactiveProperty<bool>(true);
    public ReadOnlyReactiveProperty<bool> IsAvailabl { get { return _isAvailable; } }

    public int BulletPower = 1;

    public BulletModel(){
        SetAvailabl(true);
    }

    public void SetCommentChar(char newCommentChar){
        _commentChar.Value = newCommentChar;
    }

    public void SetAvailabl(bool isAvailable){
        _isAvailable.Value = isAvailable;
    }
}
