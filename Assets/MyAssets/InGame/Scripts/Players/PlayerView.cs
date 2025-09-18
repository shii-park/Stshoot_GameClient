using UnityEngine;
using R3;
using DG.Tweening;

namespace StShoot.InGame.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]private PlayerCore _playerCore;
        [SerializeField] private GameObject _player;
        
        void Start()
        {
            _playerCore.IsDead
                .Subscribe(isDead =>
                {
                    if (isDead)
                    {
                        Hide();
                    }
                    else
                    {
                        Debug.Log("hoge");
                        Show();
                    }
                });
        }

        void Hide()
        {
            _player.transform.DOScaleX(0, 0.2f).SetEase(Ease.OutCirc);
        }
        
        void Show()
        {
            _player.transform.DOScaleX(0.12f, 0.2f).SetEase(Ease.OutCirc);
        }
    }
}
