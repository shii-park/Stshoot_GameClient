using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace StShoot.InGame.UIs
{
    public class CommentUIView : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject commentPrefab;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private int maxVisibleCount = 7;
        

        private Queue<GameObject> commentQueue = new Queue<GameObject>();

        public void AddComment(string userName, string comment)
        {
            GameObject obj = Instantiate(commentPrefab, content);
            obj.GetComponent<CommentItem>().Set(userName, comment);
            commentQueue.Enqueue(obj);

            // 最大表示数を超えたら古いコメントを削除
            if (commentQueue.Count > maxVisibleCount)
            {
                Destroy(commentQueue.Dequeue());
            }

            // 一番下まで自動スクロール
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}
