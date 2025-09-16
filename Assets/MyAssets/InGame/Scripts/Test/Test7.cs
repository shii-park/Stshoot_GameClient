using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace StShoot
{
    public class Test7 : MonoBehaviour
    {
        public static int port = 5678;
        private WebSocketServer server;

        void Start()
        {
            server = new WebSocketServer(port);
            server.AddWebSocketService<Echo>("/");
            server.Start();

        }
        void OnDestroy()
        {
            server.Stop();
            server = null;
        }
    }

    public class Echo : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Log(e.Data.ToString());
            Sessions.Broadcast(e.Data);
        }

    }
}
