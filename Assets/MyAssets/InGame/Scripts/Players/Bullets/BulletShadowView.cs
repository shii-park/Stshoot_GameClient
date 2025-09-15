using UnityEngine;
using DG.Tweening;
using TMPro;

namespace StShoot.InGame.Players.Bullets
{
    /// <summary>
    /// 弾の影を表示するクラス
    /// </summary>
    public class BulletShadowView : MonoBehaviour
    {
        private void Start()
        {
            this.gameObject.transform.DOScale(new Vector3(2.3f, 2.3f, 2.3f), 0.3f).SetEase(Ease.OutQuad);
            this.gameObject.GetComponent<TextMeshPro>().DOFade(0, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => Destroy(this.gameObject));
        }
    }
}
