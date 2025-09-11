using UnityEngine;
using TMPro;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    GameObject _hoge;

    [SerializeField]
    private string _text;

    void Start()
    {
        var prefab = Instantiate(_hoge);
        TextMeshPro countText = prefab.GetComponentInChildren<TextMeshPro>();
        countText.text = _text;
    }
}
