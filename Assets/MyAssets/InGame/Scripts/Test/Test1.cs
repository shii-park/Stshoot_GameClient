using UnityEngine;
using TMPro;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    GameObject _hoge;

    [SerializeField]
    private string _text;

    [SerializeField]
    private Transform pos;
    
    BulletPresenter pre;

    void Start()
    {
        Vector3 vec = pos.position;
        var prefab = Instantiate(_hoge,vec,Quaternion.identity);
        pre = prefab.GetComponent<BulletPresenter>();
        pre.Model.SetAvailabl(false);
        pre.Model.SetCommentChar(_text);

        Invoke(nameof(Keika), 5.0f);
    }

    void Keika(){
        pre.Model.SetAvailabl(true);
        Invoke(nameof(Sarani), 3.0f);
    }

    void Sarani(){
        pre.Model.SetAvailabl(false);
        pre.Model.SetCommentChar("あ");
    }
}
