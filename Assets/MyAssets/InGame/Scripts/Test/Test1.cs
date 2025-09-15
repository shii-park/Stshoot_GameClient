using UnityEngine;
using TMPro;
using System.Collections;
using StShoot.InGame.Players.Bullets;
using UnityEngine.Serialization;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    GameObject _bulletGameObject;

    [SerializeField]
    private string _commentText;

    [SerializeField]
    private Transform _playerPos;
    
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
            Vector3 vec = _playerPos.position;
            var prefab = Instantiate(_bulletGameObject,vec,Quaternion.identity);
            var pre = prefab.GetComponent<BulletPresenter>();
            var move = prefab.GetComponent<BulletMove>();
            pre.Model.SetAvailable(false);
            pre.Model.SetCommentChar(_commentText);

            move.MoveBullet();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Keika(){
        //pre.Model.SetAvailable(true);
        Invoke(nameof(Sarani), 3.0f);
    }

    void Sarani(){
        //pre.Model.SetAvailable(false);
        //pre.Model.SetCommentChar("あ");
    }
    
}
