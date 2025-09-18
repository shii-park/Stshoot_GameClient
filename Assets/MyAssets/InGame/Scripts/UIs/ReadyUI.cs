using DG.Tweening;
using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;
using UnityEngine.UI;

namespace StShoot.InGame.UIs
{
    public class ReadyUI : MonoBehaviour
    {
        [SerializeField]
        private Text _stageText;
        
        [SerializeField]
        private Text _musicText;
        
        public void SetReadyUI(string stageLevel)
        {
            _stageText.text = $"24時間配信地獄\n〜{stageLevel}の道〜";
            _musicText.text = $"♩ 命巡りて";
            
            MainGameManager.Instance.CurrentGameState.Where(state => state == GameState.Ready).Subscribe(_ =>
            {
                _stageText.DOFade(1f,2f);
                _stageText.rectTransform.DORotate(new Vector3(720f, 0, 0), 2f,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetDelay(0.5f);
                _stageText.DOFade(0f,2f).SetDelay(3f);

                _musicText.DOFade(1f, 2f);
                _musicText.DOFade(0f, 2f).SetDelay(3f);
            });
        }
    }
}
