using UnityEngine;
using UnityEngine.UI;

namespace StShoot.InGame.UIs
{
    public class CommentItem : MonoBehaviour
    {
        [SerializeField] private Text userNameText;
        [SerializeField] private Text commentText;

        public void Set(string userName, string comment)
        {
            Debug.Log(userName + ": " + comment);
            userNameText.text = userName;
            commentText.text = comment;
        }
    }
}
