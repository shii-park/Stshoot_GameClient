using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StShoot.InGame.UIs
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private Text _topScoreText;
        [SerializeField]
        private Text _currentScoreText;
        
        private int _currentScore;

        private int _displayScore;

        private void Start()
        {
            _displayScore = 0;
            StartCoroutine(SetScoreCoroutine());
        }

        private IEnumerator SetScoreCoroutine()
        {
            while (true)
            {
                if (_displayScore < _currentScore)
                {
                    _displayScore += 200;
                    _currentScoreText.text = _displayScore.ToString("D9");
                }
                
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void SetTopScore(int topScore)
        {
            _topScoreText.text = topScore.ToString("D9");
        }
        
        public void SetCurrentScore(int currentScore)
        {
            _currentScore = currentScore;
        }
    }
}
