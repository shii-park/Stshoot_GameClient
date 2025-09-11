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

    void Start()
    {
        Vector3 vec = pos.position;
        var prefab = Instantiate(_hoge,vec,Quaternion.identity);
        TextMeshPro countText = prefab.GetComponentInChildren<TextMeshPro>();
        countText.text = _text;
    }
}
