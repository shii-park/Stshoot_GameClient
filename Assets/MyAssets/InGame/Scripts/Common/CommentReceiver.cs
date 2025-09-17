using System.Collections;
using StShoot.InGame.Players;
using UnityEngine;
using WebSocketSharp;

namespace StShoot.InGame.Common
{
    public class CommentReceiver : MonoBehaviour
    {
        [System.Serializable]
        public class UserData
        {
            public string username;
            public string text;
            public int price;
        }
        
        [SerializeField]
        private PlayerBullet _bullet;
        
        private WebSocket ws;
        private int maxRetry = 10;

        public void StartWebsocket(string url)
        {
            StartCoroutine(ConnectWhenServerReady(url));
        }

        private IEnumerator ConnectWhenServerReady(string url)
        {
            int retryCount = 0;
            while (retryCount < maxRetry)
            {
                ws = new WebSocket(url);

                ws.OnOpen += (sender, e) =>
                {
                    Debug.Log("WebSocket Connected");
                };

                ws.OnError += (sender, e) =>
                {
                    Debug.Log("WebSocket Error: " + e.Message);
                };

                ws.OnClose += (sender, e) =>
                {
                    Debug.Log("WebSocket Closed: " + e.Reason);
                };
            
                ws.OnMessage += (sender, e) =>
                {
                    UserData data = JsonUtility.FromJson<UserData>(e.Data);
                    Debug.Log($"受信メッセージ: {e.Data}");
                    _bullet.AddReadyComments(data.text);
                };

                ws.Connect();

                if (ws.IsAlive)
                    yield break;

                ws.Close();
                ws = null;

                retryCount++;
                Debug.Log($"リトライ {retryCount}/{maxRetry}");
                yield return new WaitForSeconds(1.0f);
            }

            Debug.LogWarning("サーバーに接続できませんでした。");
        }

        void OnDestroy()
        {
            ws?.Close();
            ws = null;
        }
    }
}
