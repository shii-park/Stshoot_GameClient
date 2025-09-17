using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StShoot.InGame.UIs
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private GameObject _clearText;
        [SerializeField]
        private GameObject _gameOverText;

        [SerializeField]
        private Text _lifePointText;
        
        [SerializeField]
        private Text _priceText;
        
        [SerializeField]
        private Text _currentScoreText;
        
        [SerializeField]
        private Text _totalScoreText;
        
        [SerializeField]
        private Text _pressSpaceKeyImage;
        
        public void SetResultUI(bool isClear, int lifePoint, int price, int currentScore, int totalScore)
        {
            _clearText.SetActive(isClear);
            _gameOverText.SetActive(isClear == false);

            SetLifePoint(lifePoint);
            SetPrice(price);
            SetCurrentScore(currentScore);
            SetTotalScore(totalScore);
        }
        
        private void SetLifePoint(int lifePoint)
        {
            _lifePointText.text = $"{lifePoint} × 30000 = {lifePoint * 30000}";
        }
        
        private void SetPrice(int price)
        {
            _priceText.text = price.ToString("N0");
        }
        
        private void SetCurrentScore(int currentScore)
        {
            _currentScoreText.text = currentScore.ToString("D9");
        }
        
        private void SetTotalScore(int totalScore)
        {
            _totalScoreText.text = totalScore.ToString("D9");
        }

        public void ShowUI()
        {
            ShowPressSpaceKey();
            this._canvasGroup.DOFade(1.0f, 0.5f);
        }
        
        public void HideUI()
        {
            this._canvasGroup.DOFade(0.0f, 0.5f);
        }
        
        public void ShowPressSpaceKey()
        {
            _pressSpaceKeyImage.DOFade(0.0f, 2f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
