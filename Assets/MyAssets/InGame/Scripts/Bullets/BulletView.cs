using UnityEngine;
using TMPro;

namespace Assets.MyAssets.InGame.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        TextMeshPro countText;
        public void SetText(string commentChar){
            countText.text = commentChar;
        }

        public void SetActive(bool isActive){
            gameObject.SetActive(isActive);
        }
    }
}
