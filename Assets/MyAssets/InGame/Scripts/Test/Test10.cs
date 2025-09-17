using System.Collections;
using StShoot;
using StShoot.InGame.Common;
using UnityEngine;
using UnityEngine.Networking;

public class Test10 : MonoBehaviour
{
    [SerializeField] private CommentReceiver _commentReceiver;
    
    [SerializeField] RoomIDView _roomIDView;
    
    [System.Serializable]
    public class RoomID
    {
        public string roomID;
    }

    void Start()
    {
        StartCoroutine(GetRequest("https://stshoot-backend.onrender.com/create"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;

                // JSONをC#クラスに変換
                RoomID data = JsonUtility.FromJson<RoomID>(json);
                _roomIDView.SetRoomID(data.roomID);
                _commentReceiver.StartWebsocket($"wss://stshoot-backend.onrender.com/ws/receiver/{data.roomID}");
            }
        }
    }
}