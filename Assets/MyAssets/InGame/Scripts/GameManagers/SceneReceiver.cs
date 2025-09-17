using StShoot.Common;
using StShoot.Common.Scripts;
using StShoot.InGame.Common;
using UnityEngine;
using UnityEngine.UI;

namespace StShoot.InGame.GameManagers
{
    public class SceneReceiver : MonoBehaviour
    {
        [SerializeField]
        private CommentReceiver _commentReceiver;
        
        [SerializeField]
        private Text _roomIdText;

        public void Init()
        {
            var value = SceneTransitionManager.GetPassedValue<GameSetting>();
            _commentReceiver.StartWebsocket(value.RoomID);

            _roomIdText.text = value.RoomID;
        }
    }
}
