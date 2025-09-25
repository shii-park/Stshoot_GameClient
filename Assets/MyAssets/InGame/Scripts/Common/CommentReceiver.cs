using System.Collections;
using System.Collections.Concurrent;
using StShoot.InGame.Players;
using StShoot.InGame.UIs;
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
        
        [SerializeField]
        private CommentUIView _commentUIView;
        
        private WebSocket ws;
        private int maxRetry = 10;
        
        private ConcurrentQueue<UserData> _uiQueue = new ConcurrentQueue<UserData>();
        
        private string _wsUrl = "wss://stshoot-backend.onrender.com/ws/receiver/";

        [SerializeField]
        private bool _isDebug;
        
        private void Awake()
        {
            _uiQueue = new ConcurrentQueue<UserData>();
        }

        private void Update()
        {
            while (_uiQueue.TryDequeue(out var data))
            {
                _commentUIView.AddComment(data.username, data.text);
            }
        }

        public void StartWebsocket(string roomID)
        {
            var url = _wsUrl + roomID;
# if UNITY_EDITOR
            if (_isDebug)
            {
                StartCoroutine(DebugComment());
            }
            else
            {
                StartCoroutine(ConnectWhenServerReady(url));
            }
            return;
# endif
            StartCoroutine(ConnectWhenServerReady(url));
        }
        
# if UNITY_EDITOR

        private IEnumerator DebugComment()
        {
            string commentList = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん";

            while (true)
            {
                yield return new WaitForSeconds(2.0f);
                _bullet.AddReadyComments(commentList);
            }
            
        }

# endif

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
                    _bullet.AddReadyComments(data.text);

                    if (data != null && !string.IsNullOrEmpty(data.username) && !string.IsNullOrEmpty(data.text))
                    {
                        _uiQueue.Enqueue(data);
                    }
                    else
                    {
                        Debug.LogWarning("受信データが不正です: " + e.Data);
                    }
                };

                ws.Connect();

                if (ws.IsAlive)
                    yield break;

                ws.Close();
                ws = null;

                retryCount++;
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
