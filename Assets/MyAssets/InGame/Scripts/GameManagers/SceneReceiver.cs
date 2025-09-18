using StShoot.Common;
using StShoot.Common.Scripts;
using StShoot.InGame.Common;
using StShoot.InGame.UIs;
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
        
        [SerializeField]
        private ReadyUI _readyUI;

        public void Init()
        {
            var value = SceneTransitionManager.GetPassedValue<GameSetting>();
            _commentReceiver.StartWebsocket(value.RoomID);
            
            _readyUI.SetReadyUI(value.Level.ToString());

            _roomIdText.text = value.RoomID;
        }
    }
}
