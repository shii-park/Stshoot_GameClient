using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace StShoot.OutGame
{
    public class GameRoom : MonoBehaviour
    {
        string _roomID;
        public string RoomId => _roomID;
        
        [System.Serializable]
        public class RoomID
        {
            public string roomID;
        }
        
        private void Awake()
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
                    _roomID = JsonUtility.FromJson<RoomID>(json).roomID;
                }
            }
        }
    }
}
