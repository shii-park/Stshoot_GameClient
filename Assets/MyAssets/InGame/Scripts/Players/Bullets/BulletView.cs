using UnityEngine;
using TMPro;

namespace StShoot.InGame.Scripts.Players.Bullets
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
