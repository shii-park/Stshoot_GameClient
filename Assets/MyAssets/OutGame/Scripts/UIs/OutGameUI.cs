using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StShoot.OutGame.UIs
{
    public class OutGameUI : MonoBehaviour
    {
        [SerializeField]
        private Text _titleFirstHalfText;
        [SerializeField]
        private Text _titleSecondHalfText;
        [SerializeField]
        private Text _startButtonText;
        
        [SerializeField]
        private RectTransform _titleFirstHalfRectTransform;
        [SerializeField]
        private RectTransform _titleSecondHalfRectTransform;
        
        [SerializeField]
        private RectTransform _menuItemsRectTransform;
        
        [SerializeField]
        private RectTransform _roomIDRectTransform;
        
        [SerializeField]
        private Text _roomIDText;
        
        [SerializeField]
        private Text _readyText;

        public void StartTitleAnimation()
        {
            _titleFirstHalfText.rectTransform.DOAnchorPos(_titleFirstHalfRectTransform.anchoredPosition, 1f).SetEase(Ease.OutBack);
            _titleSecondHalfText.rectTransform.DOAnchorPos(_titleSecondHalfRectTransform.anchoredPosition, 1f).SetEase(Ease.OutBack);
            _startButtonText.DOFade(0, 1f);
            
        }
        
        public void ShowMenuItems()
        {
            _menuItemsRectTransform.DOAnchorPosX(0, 1f).SetEase(Ease.OutBack);
        }

        public void ShowRoomID(string roomID)
        {
            _roomIDText.text = roomID;
            _roomIDRectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce);
            _readyText.DOFade(0.0f, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
