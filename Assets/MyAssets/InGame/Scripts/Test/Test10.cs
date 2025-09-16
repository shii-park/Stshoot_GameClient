using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Test10 : MonoBehaviour
{
    [System.Serializable]
    public class UserData
    {
        public string roomID;
    }

    void Start()
    {
        StartCoroutine(GetRequest("http://xn--ip-573a5a0e5c/create"));
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
                Debug.Log("Response: " + json);

                // JSONをC#クラスに変換
                UserData data = JsonUtility.FromJson<UserData>(json);
                Debug.Log($"Name: {data.roomID}");
            }
        }
    }
}