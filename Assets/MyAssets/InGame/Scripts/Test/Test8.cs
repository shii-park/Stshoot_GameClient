using System.Collections;
using UnityEngine;
using WebSocketSharp;

public class WaitForServer : MonoBehaviour
{
    private string url = "ws://192.0.0.2:8080/ws";
    private WebSocket ws;
    private int maxRetry = 10;

    void Start()
    {
        StartCoroutine(ConnectWhenServerReady());
    }

    private IEnumerator ConnectWhenServerReady()
    {
        int retryCount = 0;
        while (retryCount < maxRetry)
        {
            ws = new WebSocket(url);

            ws.OnOpen += (sender, e) =>
            {
                Debug.Log("WebSocket Open");
                ws.Send("Hello!");
            };

            ws.OnError += (sender, e) =>
            {
                Debug.Log("WebSocket Error: " + e.Message);
            };

            ws.OnClose += (sender, e) =>
            {
                Debug.Log("WebSocket Closed: " + e.Reason);
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