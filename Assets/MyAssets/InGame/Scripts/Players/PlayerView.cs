using System.Collections;
using UnityEngine;
using R3;
using DG.Tweening;

namespace StShoot.InGame.Players
{
    /// <summary>
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerCore _playerCore;
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
                        Show();
                    }
                });

            _playerCore.IsInvincible.Where(isInvincible => isInvincible)
                .Subscribe(_ =>
                {
                    StartCoroutine(Blink());
                });

            void Hide()
            {
                _player.transform.DOScaleX(0, 0.2f).SetEase(Ease.OutCirc);
            }

            void Show()
            {
                _player.transform.DOScaleX(0.12f, 0.2f).SetEase(Ease.OutCirc);
            }

            IEnumerator Blink()
            {
                var currentColor = _player.GetComponent<SpriteRenderer>().color;
                for(int i = 0; i < 10; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    _player.GetComponent<SpriteRenderer>().color = new Color (currentColor.r, currentColor.r, currentColor.b, 0);
                    yield return new WaitForSeconds(0.1f);
                    _player.GetComponent<SpriteRenderer>().color = currentColor;
                }
            }
        }
    }
}