using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

namespace StShoot.Common.Scripts
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }

        private static object _passedValue;

        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private float _fadeDuration = 1.0f;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(_fadeCanvasGroup.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            FadeIn(null);
        }

        public void LoadScene(string sceneName, object value = null, Action onComplete = null)
        {
            _passedValue = value;
            FadeOut(() =>
            {
                SceneManager.LoadScene(sceneName);
                FadeIn(onComplete);
            });
        }

        public static T GetPassedValue<T>()
        {
            if (_passedValue is T t)
            {
                var result = t;
                _passedValue = null; 
                return result;
            }
            return default;
        }

        private void FadeOut(Action onComplete)
        {
            _fadeCanvasGroup.DOFade(1, _fadeDuration).OnComplete(() => onComplete?.Invoke());
        }

        private void FadeIn(Action onComplete)
        {
            _fadeCanvasGroup.DOFade(0, _fadeDuration).OnComplete(() => onComplete?.Invoke());
        }
    }
}
