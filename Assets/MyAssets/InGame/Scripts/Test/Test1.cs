using UnityEngine;
using TMPro;
using System.Collections;
using StShoot.InGame.Scripts.Players.Bullets;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    GameObject _hoge;

    [SerializeField]
    private string _text;

    [SerializeField]
    private Transform pos;
    
    //BulletPresenter pre;

    //BulletMove move;

    void Start()
    {
        StartCoroutine(Seisei());
    }

    IEnumerator Seisei()
    {
        // 条件にゲームの状態を増やす
        while (true)
        {
            Vector3 vec = pos.position;
            var prefab = Instantiate(_hoge,vec,Quaternion.identity);
            var pre = prefab.GetComponent<BulletPresenter>();
            var move = prefab.GetComponent<BulletMove>();
            pre.Model.SetAvailable(false);
            pre.Model.SetCommentChar(_text);

            move.MoveBullet();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Keika(){
        //pre.Model.SetAvailabl(true);
        Invoke(nameof(Sarani), 3.0f);
    }

    void Sarani(){
        //pre.Model.SetAvailabl(false);
        //pre.Model.SetCommentChar("あ");
    }
    
}
