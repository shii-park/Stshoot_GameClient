using UnityEngine;
using UnityEngine.UI;

namespace StShoot
{
    public class RoomIDView : MonoBehaviour
    {
        [SerializeField]
        private Text _roomIDText;
        
        public void SetRoomID(string roomID)
        {
            _roomIDText.text = roomID;
        }
    }
}
