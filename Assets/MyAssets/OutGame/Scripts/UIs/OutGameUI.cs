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

        public void StartTitleAnimation()
        {
            _titleFirstHalfText.rectTransform.DOAnchorPos(_titleFirstHalfRectTransform.anchoredPosition, 1f).SetEase(Ease.OutBack);
            _titleSecondHalfText.rectTransform.DOAnchorPos(_titleSecondHalfRectTransform.anchoredPosition, 1f).SetEase(Ease.OutBack);
            _startButtonText.DOFade(0, 1f);
        }
    }
}
